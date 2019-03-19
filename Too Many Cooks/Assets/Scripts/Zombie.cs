using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    #region targetVariables
    public Transform player;
    public bool attackPlayer;
    #endregion

    #region movementVariables
    Rigidbody2D enemyRB;
    public float moveSpeed;
    #endregion

    #region healthVariables
    public float maxHealth;
    public float currHealth;
    #endregion

    #region attackVariables
    public float attackPower;
    public float attackRadius;
    public float attackTime;
    private float lastDamage;
    #endregion


    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
    }

    // Use this for initialization
    void Update()
    {
        // Check to see if zombie is dead
        if (currHealth <= 0) {
            die();
            /* TODO: Play death animation?? */
        }
        // Check to see if we know where player is
        if (player == null)
        {
            return;
        }

        Move();
    }

    private void FixedUpdate()
    {
        if (lastDamage >= attackTime) {
            lastDamage = 0;
        }
        lastDamage += Time.fixedDeltaTime;
    }


    private void Move()
    {
        // Calculate the movement vector. Player pos - Enemy pos = Direction of player relative to enemy
        Vector2 direction = player.position - transform.position;
        enemyRB.velocity = direction.normalized * moveSpeed;
    }


    #region healthFunctions
    public void TakeDamage(float damage) {
        currHealth -= damage;
    }

    private void die() {
        Destroy(gameObject);
    }
    #endregion


    #region attackFunctions
    private void Attack()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackRadius, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                // deal damage
                hit.transform.GetComponent<PlayerHealth>().TakeDamage(attackPower);
                Debug.Log("ATTACKED PLAYER AT " + lastDamage + " TIME");
                lastDamage = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            /* TODO: Play attack animation? */
            Debug.Log("ENTERED COLLISION ZONE");
            attackPlayer = true;
            Attack();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            /* TODO: Play attack animation? */
            attackPlayer = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attackPlayer && lastDamage >= attackTime) {
            Debug.Log("STAYED IN COLLISION ZONE");
            Attack();
        }
    }
    #endregion

}
