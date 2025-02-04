using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Slider powerBar;

    private int currentScore = 0;

    void Start()
    {
        UpdateScoreUI();
    }

    public void IncreaseScore()
    {
        currentScore++;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + currentScore;
        Debug.Log("Score: " + currentScore);
    }

    public void UpdatePowerBar(float value)
    {
        powerBar.value = value; // Updates power bar UI
    }
}
