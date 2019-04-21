using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    #region UnityFunctions
    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion


    #region SceneTransitions
    public void StartGame() {
        SceneManager.LoadScene("TestScene");
    }


    public void LoseGame() {
        SceneManager.LoadScene("DieScene");
    }
    #endregion
}
