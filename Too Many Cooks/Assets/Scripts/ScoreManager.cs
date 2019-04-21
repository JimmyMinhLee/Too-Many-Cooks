using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region ScoreVariables
    public int dishesServed;
    public int totalTips;
    #endregion

    public Text scoreText;

    private void Start()
    {
        dishesServed = 0;
        totalTips = 0;
        //scoreText.text = dishesServed.ToString();
        scoreText.text = "$" + totalTips.ToString();
    }


    public void updateDishesServed(int numDishes) {
        dishesServed += numDishes;
        //scoreText.text = dishesServed.ToString();
    }

    public void accumulateTips(int tip) {
        totalTips += tip;
        scoreText.text = "$" + totalTips.ToString();
    }
}
