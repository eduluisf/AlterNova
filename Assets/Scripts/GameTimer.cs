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

    private bool isTimeRunning = true; // Nuevo: Controla si el tiempo está corriendo o no

    private void Update()
    {
        if (isTimeRunning)
        {
            // Incrementa el tiempo del juego en segundos
            gameTime += Time.deltaTime;
            int timeint = (int)Math.Round(gameTime);
            buttonText.text = timeint.ToString();

            // Verifica si han pasado 5 segundos desde la última vez que se restó el puntaje
            if (gameTime - lastScoreDecreaseTime >= scoreDecreaseInterval)
            {
                DecreaseScore();
                lastScoreDecreaseTime = gameTime;
            }
        }
    }

    // Método que retorna el tiempo de juego actual en segundos
    public float GetGameTime()
    {
        return gameTime;
    }

    // Método para restar -10 al puntaje
    private void DecreaseScore()
    {
        gameResult.setScore(-5);
    }

    // Nuevo: Método para detener el tiempo desde otro script
    public void StopTime()
    {
        isTimeRunning = false;
    }
}
