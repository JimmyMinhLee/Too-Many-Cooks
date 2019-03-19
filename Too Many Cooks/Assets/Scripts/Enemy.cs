using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region enemyVariables
    float health;
    #endregion 
    // Start is called before the first frame update
    void Start()
    {
        health = 100; 
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this); 
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage; 
    }
}
