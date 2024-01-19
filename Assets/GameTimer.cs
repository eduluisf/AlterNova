using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float gameTime = 0f;
    [SerializeField]
    private GameResult gameResult;

    private float lastScoreDecreaseTime = 0f;
    private float scoreDecreaseInterval = 5f;

    private void Update()
    {
        // Incrementa el tiempo del juego en segundos
        gameTime += Time.deltaTime;

        // Verifica si han pasado 5 segundos desde la última vez que se restó el puntaje
        if (gameTime - lastScoreDecreaseTime >= scoreDecreaseInterval)
        {
            DecreaseScore();
            lastScoreDecreaseTime = gameTime;
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
       gameResult.setScore(-10);
     
    }
}
