using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public score scoreManager; 
    public Slider powerBar;
    public paper paperScript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RG();
        }

        if (powerBar && paperScript)
        {
            powerBar.value = paperScript.throwForce / 10f; // Normalize throw force (Assuming max is 10)
        }
    }

    void ResetGame()
    {
        paperScript.ResetBall();
        scoreManager.AddScore();
    }
    void RG()
    {
        paperScript.ResetBall();
    }
}
