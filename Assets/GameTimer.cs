using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float gameTime = 0f;

    private void Update()
    {
        // Incrementa el tiempo del juego en segundos
        gameTime += Time.deltaTime;
    }

    // Método que retorna el tiempo de juego actual en segundos
    public float GetGameTime()
    {
        return gameTime;
    }
}
