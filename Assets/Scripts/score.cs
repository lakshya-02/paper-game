/*This Script is mainly for UI as Text and PowerBar is there and 
score is define but proper logic isn't  */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Text scoreText;
    public Slider powerBar;

    private int currentScore = 0;

    void Start()
    {
        UpdateScoreUI();
        if (powerBar)
            powerBar.value = 0;
    }

    public void AddScore()
    {
        currentScore++;
        UpdateScoreUI();
    }

    public void UpdatePowerBar(float value)
    {
        if (powerBar)
            powerBar.value = value;
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + currentScore;
        Debug.Log("Score: " + currentScore);
    }
}
