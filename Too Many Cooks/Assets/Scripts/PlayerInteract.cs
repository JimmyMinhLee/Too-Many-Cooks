using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    #region InteractableObjects
    public GameObject currObj;
    RaycastHit2D[] hits = new RaycastHit2D[0];
    public float distance = 3f;
    public bool pressedEelsewhere;
    #endregion


    #region GrabbedObjects
    public bool grabbed;
    public Transform holdPoint;
    public Transform rightHoldPoint;
    public Transform leftHoldPoint;
    public Transform upHoldPoint;
    public Transform downHoldPoint;
    public Transform upRightHoldPoint;
    public Transform upLeftHoldPoint;
    public Transform downRightHoldPoint;
    public Transform downLeftHoldPoint;
    #endregion


    #region PlayerControllerVariables
    Animator playerAnim;
    public float moveSpeed;
    #endregion


    #region InitialFunctions
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        moveSpeed = GameObject.Find("Player").GetComponent<PlayerController>().moveSpeed;
        holdPoint = downHoldPoint;
    }


    void Update() {
        float x = playerAnim.GetFloat("XDirection");
        float y = playerAnim.GetFloat("YDirection");

        if (currObj) {
            ChangeHoldPoint(x, y);
        }
        CheckInteract();
    }
    #endregion


    #region InteractFunctions
    void CheckInteract() {
        // Check if the player is in the zone of an item and if the player clicks "E"/"Interact"
        if (Input.GetButtonDown("Interact") && currObj && !pressedEelsewhere) {
            // Grabs the item and puts it in the player's hands
            if (!grabbed)
            {
                currObj.transform.position = holdPoint.position;
                currObj.GetComponent<SpriteRenderer>().sortingLayerName = "HeldIngredient";
                grabbed = true;
            }
            else
            {
                // Cast a ray in the direction of the key pressed 
                // Check if the ray collides with a counter/cooking station/plate
                // If it does, change its position to the position of that
                if (Input.GetKey("a") || Input.GetKey("left"))
                {
                    hits = Physics2D.RaycastAll(holdPoint.position + Vector3.down, Vector2.left * transform.localScale.x, distance);
                }
                if (Input.GetKey("d") || Input.GetKey("right"))
                {
                    hits = Physics2D.RaycastAll(holdPoint.position + Vector3.down, Vector2.right * transform.localScale.x, distance);
                }
                if (Input.GetKey("w") || Input.GetKey("up"))
                {
                    hits = Physics2D.RaycastAll(holdPoint.position, Vector2.up * transform.localScale.x, distance);
                }
                if (Input.GetKey("s") || Input.GetKey("down"))
                {
                    hits = Physics2D.RaycastAll(holdPoint.position, Vector2.down * transform.localScale.x, 1f+ distance);
                }

                // Drop item on the counter if the ray hit a counter and there isn't already an item there
                bool hitCounter = false;
                bool hitInteractable = false;
                Transform hitObj = null;
                if (hits.Length > 0) {
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider != null && hit.collider.gameObject.tag == "Counter" && !hitCounter)
                        {
                            hitObj = hit.collider.gameObject.transform;
                            hitCounter = true;
                            hits = new RaycastHit2D[0];
                        }
                        // || hit.collider.gameObject.tag == "Plate"
                        if (hit.collider != null && 
                            (hit.collider.gameObject.tag == "Ingredient" || hit.collider.gameObject.tag == "Dish" || hit.collider.gameObject.tag == "Uncookable") &&
                            hit.collider.gameObject.transform != currObj.transform) {
                            hitInteractable = true;
                        }
                    }
                }


                // Drops the item
                if (!hitCounter) {
                    currObj.transform.position = holdPoint.position + Vector3.down;
                } 
                if (hitObj != null && !hitInteractable) {
                    currObj.transform.position = hitObj.position;
                }

                grabbed = false;
                currObj.GetComponent<SpriteRenderer>().sortingLayerName = "Ingredient";

            }
        }

        // If the player is currently grabbing an object, have the object be held in their hands
        if (grabbed && currObj) {
            currObj.transform.position = holdPoint.position;
            pressedEelsewhere = false;
        }
    }


    void ChangeHoldPoint(float x, float y) {
        float compx = 1f * moveSpeed;
        float compy = 1f * moveSpeed;

        if (grabbed && y == -compy)
        {
            currObj.GetComponent<SpriteRenderer>().sortingLayerName = "HeldIngredient";
        } else if (grabbed && y != -compy) {
            currObj.GetComponent<SpriteRenderer>().sortingLayerName = "Ingredient";
        }

        // Player is facing right
        if (x == compx && y == 0) {
            holdPoint = rightHoldPoint;
        }
        // Player is facing left
        if (x == -compx && y == 0) {
            holdPoint = leftHoldPoint;
        }
        // Player is facing up
        if (x == 0 && y == compy) {
            holdPoint = upHoldPoint;
        }
        // Player is facing down
        if (x == 0 && y == -compy) {
            holdPoint = downHoldPoint;
        }
        // Player is facing up/right
        if (x == compx && y == compy) {
            holdPoint = upRightHoldPoint;
        }
        // Player is facing up/left
        if (x == -compx && y == compy) {
            holdPoint = upLeftHoldPoint;
        }
        // Player is facing down/right
        if (x == compx && y == -compy) {
            holdPoint = downRightHoldPoint;
        }
        // Player is facing down/left
        if (x == -compx && y == -compy) {
            holdPoint = downLeftHoldPoint;
        }
    }
    #endregion


    #region TriggerFunctions
    void OnTriggerEnter2D(Collider2D obj) {
        // Enter collider of an ingredient
        if (obj.CompareTag("Ingredient") && currObj == null) {
            currObj = obj.gameObject;
        }
        if (obj.CompareTag("Dish") && currObj == null)
        {
            currObj = obj.gameObject;
        }
        if (obj.CompareTag("Plate") && currObj == null)
        {
            currObj = obj.gameObject;
        }
        if (obj.CompareTag("Uncookable") && currObj == null)
        {
            currObj = obj.gameObject;
        }
    }


    void OnTriggerExit2D(Collider2D obj) {
        // Exit collider of an ingredient
        if (obj.CompareTag("Ingredient")) {
            if (obj.gameObject == currObj) {
                currObj = null;
            }
        }
        if (obj.CompareTag("Dish"))
        {
            if (obj.gameObject == currObj)
            {
                currObj = null;
            }
        }
        if (obj.CompareTag("Plate"))
        {
            if (obj.gameObject == currObj)
            {
                currObj = null;
            }
        }
        if (obj.CompareTag("Uncookable"))
        {
            if (obj.gameObject == currObj)
            {
                currObj = null;
            }
        }
    }
    #endregion
}
