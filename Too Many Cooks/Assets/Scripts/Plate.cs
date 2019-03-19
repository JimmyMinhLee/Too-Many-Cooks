using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    #region Plate Variables
    private bool hasPlayer;
    private bool hasCooked;
    private string dish;
    private string cookedIngredient;
    #endregion

    //Get the GameObject of the player and ingredient.
    #region Cooking GameObjects
    protected GameObject playerObj;
    protected GameObject cookedObj;
    #endregion

    #region Array of dishes
    private string[] dishes = { "ZombieLegDish"};
    #endregion

    #region Trigger Functions   

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        //Set the corresponding boolean value true for each contacted collider.
        string colTag = collider.transform.tag;

        switch (colTag)
        {
            case "Player":
                hasPlayer = true;
                playerObj = collider.gameObject;
                //Debug.Log("Player entered");
                break;
            case "Ingredient":
                hasCooked = true;
                cookedObj = collider.gameObject;
                cookedIngredient = cookedObj.name;
                //Debug.Log("Ingredient entered");
                break;

            default:
                break;
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collider)
    {
        //The player can cook only when both he and the ingredient is in contact with the station.
        if (hasCooked)
        {
            //Debug.Log("The player can now cook");
            //Press 'e' to cook the ingredient.
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Is cooking");
                Assemble(cookedIngredient);
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        //Set the boolean value false when not colliding.
        string colTag = collider.transform.tag;

        switch (colTag)
        {
            case "Player":
                hasPlayer = false;
                playerObj = null;
                //Debug.Log("Player exited");
                break;

            case "Ingredient":
                hasCooked = false;
                cookedObj = null;
                //Debug.Log("Ingredient exited");
                break;

            default:
                break;
        }
    }
    #endregion Assemble Function

    private void Assemble(string cookedIngredient)
    {
        if (dish == null)
        {
            dish = cookedIngredient + "Dish";
        }
    }

    #region 

    #endregion
}
