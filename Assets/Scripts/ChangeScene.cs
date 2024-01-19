using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Nombre de la escena a la que quieres cambiar
  

    // Método para cambiar de escena
    public void ChangeToScene()
    {
        SceneManager.LoadScene("Game");
    }
}
