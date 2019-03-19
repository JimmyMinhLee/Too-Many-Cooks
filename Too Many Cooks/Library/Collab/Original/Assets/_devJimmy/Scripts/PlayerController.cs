using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region playerMoveVariables 

    Rigidbody2D playerRigidBody;
    public float xAxis;
    public float yAxis;
    public float moveSpeed; // We can increase this later on with regards to buffs, sprinting? etc.
    public Vector2 currDirection; // Used for animation
    Animator playerAnim;

    #endregion

    #region cookingObjects

    GameObject currentObject; // Used to store what the player is currently carrying - whether that be an ingredient or a finished dish 
    GameObject tool; // May be useful - if the player is currently carrying a knife, spatula, etc. 

    #endregion 

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        moveSpeed = 2.5f; // moveSpeed set to 1 by default 
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }
    /**
     * Handles movement 
     * Vector2 movementVector = new Vector2(x Input, y Input); 
     * playerRigidBody.velocity = movementVector; 
     */

    void PlayerMove()
    {

        // Handles movement 
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        Vector2 movementVector = new Vector2(xAxis, yAxis);
        playerRigidBody.velocity = movementVector;

        if (Input.GetAxisRaw("Horizontal") > 0) // Move right 
        {
            playerAnim.SetBool("IsMoving", true);
            currDirection = Vector2.right;  
        }

        if (Input.GetAxisRaw("Horizontal") < 0) // Move left 
        {
            playerAnim.SetBool("IsMoving", true);
            currDirection = Vector2.left;
        }

        if (Input.GetAxisRaw("Vertical") > 0) // Move up 
        {
            playerAnim.SetBool("IsMoving", true);
            currDirection = Vector2.up;
        }

        if (Input.GetAxisRaw("Vertical") < 0) // Move down 
        {
            playerAnim.SetBool("IsMoving", true);
            currDirection = Vector2.down;
        }

        else // No movement at all 
        {
            playerAnim.SetBool("IsMoving", false);
        }
    }


    /** If the player hits the interaction key - whatever it is... 
     *  Cast a ray in the currDirection 
     *  Check if they collide with any interactable items 
     *  Call the interact() function inside of the interactable - whether that be putting an item into a grill, 
     *  whatever it may be 
     */

    void PlayerInteract()
    {
        // Cast rays 
        // If rays collide 
        // Check if rays collide with interactable object 
        // Call interact 
    }

    /** If the player hits the attack button
     * Cast a ray in the curr direction
     * Check if they collide with any enemies
     * Call the takeDamage() function inside of the enemy 
     * 
     */

    void Attack()
    {
        // Cast rays 
        // Check ray collisions
        // If any enemies are there
        // Call takeDamage
    }
}
