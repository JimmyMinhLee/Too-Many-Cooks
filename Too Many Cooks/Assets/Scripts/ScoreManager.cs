using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region ScoreVariables
    public int dishesServed;
    #endregion 


    public void updateDishesServed(int numDishes) {
        dishesServed += numDishes;
        Debug.Log("NUM DISHES SERVED: " + dishesServed);
    }
}
