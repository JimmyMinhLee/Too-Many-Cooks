using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingStation : MonoBehaviour
{
    public GameObject dish;
    public bool hasPlayer;
    public bool hasDish;
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

            case "Dish":
                hasDish = true;
                dish = collider.gameObject;
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

            case "Dish":
                hasDish = false;
                break;

            default:
                break;
        }
    }


    protected void OnTriggerStay2D(Collider2D collider)
    {
        //The player can only throw away an ingredient if he is in range and is holding an ingredient
        if (hasPlayer && hasDish && player.GetComponent<PlayerInteract>().grabbed)
        {
            //Press 'e' to open the box
            if (Input.GetButtonDown("Interact"))
            {
                // destroy the ingredient
                Destroy(dish);
                hasDish = false;

                // destroy ingredient variables for player
                player.GetComponent<PlayerInteract>().currObj = null;
                player.GetComponent<PlayerInteract>().grabbed = false;

                // accumulate points for player
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().updateDishesServed(1);
            }
            /*TODO: Have a box opening animation and ingredient being thrown away animation?*/
        }
    }
}
