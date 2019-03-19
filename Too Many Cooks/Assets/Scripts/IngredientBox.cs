using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{
    public GameObject ingredient;
    public bool hasPlayer;
    public bool hasIngredient;
    public GameObject player;


    protected void OnTriggerEnter2D(Collider2D collider)
    {
        // set the corresponding boolean value true for each contacted collider
        string colTag = collider.transform.tag;
        switch (colTag)
        {
            case "Player":
                hasPlayer = true;
                player = collider.gameObject;
                break;

            case "Ingredient":
                hasIngredient = true;
                break;

            default:
                break;
        }
    }


    protected void OnTriggerExit2D(Collider2D collider)
    {
        // set the boolean value false when not colliding
        string colTag = collider.transform.tag;
        switch (colTag)
        {
            case "Player":
                hasPlayer = false;
                player = null;
                break;

            case "Ingredient":
                hasIngredient = false;
                break;

            default:
                break;
        }
    }


    protected void OnTriggerStay2D(Collider2D collider)
    {
        // the player can only grab an ingredient if he is in range and is not already holding an ingredient
        if (hasPlayer && !hasIngredient && !player.GetComponent<PlayerInteract>().grabbed)
        {
            // press 'e' to open the box
            if (Input.GetButtonDown("Interact"))
            {
                // set the player's current object as the ingredient
                GameObject ingredientClone = Instantiate(ingredient, player.GetComponent<PlayerInteract>().holdPoint);
                ingredientClone.name = "ZombieLeg";
                player.GetComponent<PlayerInteract>().pressedEelsewhere = true;
                player.GetComponent<PlayerInteract>().currObj = ingredientClone;
                player.GetComponent<PlayerInteract>().grabbed = true;
            }
            /*TODO: Have a box opening animation?*/
        }
    }
}
