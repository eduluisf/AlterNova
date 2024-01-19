using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameTimer : MonoBehaviour
{

    
    private float gameTime = 0f;
    [SerializeField]
    private GameResult gameResult;

    private float lastScoreDecreaseTime = 0f;
    private float scoreDecreaseInterval = 8f;

    [SerializeField] private TextMeshProUGUI buttonText;

    private bool isTimeRunning = true; //Boolean for controlling the time
    private void Update()
    {
        if (isTimeRunning)
        {
            // Increment the game time in seconds
            gameTime += Time.deltaTime;
            int timeint = (int)Math.Round(gameTime);
            buttonText.text = timeint.ToString();

           // if   the time in  lastScoreDecreaseTime  have  passed  we subtract ponts
            if (gameTime - lastScoreDecreaseTime >= scoreDecreaseInterval)
            {
                DecreaseScore();
                lastScoreDecreaseTime = gameTime;
            }
        }
    }

    // Getter for the time in seconds
    public float GetGameTime()
    {
        return gameTime;
    }

    // Substract points
    private void DecreaseScore()
    {
        gameResult.setScore(-5);
    }

    // StopTime 
    public void StopTime()
    {
        isTimeRunning = false;
    }
}
