using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingStation : MonoBehaviour
{

    private Transform stationPosition; 
    private bool canCut = false;
    public GameObject playerCurrObj;
    public GameObject player;

    public PlayerController playerMovement; 

    public float cutTiming;
    public float endAnimationTiming;

    public Animator playerAnim; 

    CuttableIngredient cutScript; // Holds the converted object 
    public bool hasPlayer;

    private void Start()
    {
        stationPosition = GetComponent<Transform>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown("e") && hasPlayer)
        {
            Interact(); 
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject coll = collider.gameObject; 
        
        if (coll.CompareTag("Player")) // If the collision that entered is the player... 
        {
            // The cutting station has a player currently there
            hasPlayer = true;
            // Gets the player and it's animator 
            player = coll;
            playerAnim = player.GetComponent<Animator>();
            playerMovement = player.GetComponent<PlayerController>(); 

            if (player.GetComponent<PlayerInteract>().currObj != null) // If the player is holding something - retrieve that item 
            {
                playerCurrObj = player.GetComponent<PlayerInteract>().currObj;
                Debug.Log("The player is holding an object"); 

                if (playerCurrObj.CompareTag("Ingredient"))
                {
                    canCut = playerCurrObj.GetComponent<Ingredient>().isCuttable;
                    cutScript = playerCurrObj.GetComponent<CuttableIngredient>();
                    Debug.Log("The player is holding an ingredient.");
                }
            }
            Debug.Log("The player has just entered");
        }

    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        GameObject coll = collider.gameObject; // I only want to do things if the object leaving is the player itself 

        if (coll.CompareTag("Player"))
        {
            Debug.Log("The player has just left"); 
            hasPlayer = false; // We don't have a player 
            playerCurrObj = null;
            player = null;
            canCut = false; 
        }
    }

    private void Interact()
    {
       if (canCut)
        {
            float cutTime = 10f;
            // Reset the necessary variables in the player i.e. what they're holding 
            PlayerInteract playerScript = player.GetComponent<PlayerInteract>();
            // Removes the player's current ingredient  
            playerScript.currObj = null;
            // The player is no longer holding an object
            Destroy(playerCurrObj); 
            playerCurrObj = null;
            // The player/station can't cut anything since we just cut an ingredient 
            canCut = false;
            playerAnim.SetBool("Cutting", true);
            StartCoroutine(Cutting());
            playerAnim.SetBool("Cutting", false);
        }

       if (canCut == false)
        {
            return; 
        }
    }

    IEnumerator Cutting()
    {
        float originalMove = playerMovement.moveSpeed;
        playerMovement.moveSpeed = 0;
        yield return new WaitForSeconds(2);
        Debug.Log("Currently cutting!");
        yield return new WaitForSeconds(endAnimationTiming);
        Instantiate(cutScript.cuttedIngredient, stationPosition);
        playerMovement.moveSpeed = originalMove;      
    }
}
