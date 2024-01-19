using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateMatrix : MonoBehaviour
{
    private Button firstClickedButton;
    private Button secondClickedButton;
    private int firstClickValue = -1;
    private int secondClickValue = -1;

    [SerializeField] private Transform buttonsContainer;
    [SerializeField] private GameObject btn;
    [SerializeField] private JsonReader jsonReader;
    private int[,] matrix;

    [SerializeField] private GameResult gameResult;


    private void Start()
    {
        StartCoroutine(EsperarJsonReader());
    }

    private IEnumerator EsperarJsonReader()
    {
        yield return new WaitUntil(() => jsonReader.GetMaxR() != 0);

        matrix = jsonReader.GetMatrix();
        int maxRValue = jsonReader.GetMaxR();
        int maxCValue = jsonReader.GetMaxC();

        createButtons(matrix);

        Debug.Log($"Desde OtroScript - El valor máximo de R es: {maxRValue}");
        Debug.Log($"Desde OtroScript - El valor máximo de C es: {maxCValue}");
    }

    private void createButtons(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject button = Instantiate(btn);
                button.name = "Button_" + (i + 1) + "_" + (j + 1);
                button.transform.SetParent(buttonsContainer, false);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
              
                buttonText.text = matrix[i, j].ToString();
                buttonText.gameObject.SetActive(false);

                Button buttonComponent = button.GetComponent<Button>();
                buttonComponent.onClick.AddListener(() => OnButtonClick(buttonComponent, int.Parse(buttonText.text), buttonText.gameObject));
            }
        }
    }
private void OnButtonClick(Button button, int value,GameObject text)
{
    gameResult.setTotalClicks();

    if (firstClickedButton == null)
    {
        HandleFirstClick(button, value,text);
    }
    else
    {
        HandleSecondClick(button, value,text);
    }
}

private void HandleFirstClick(Button button, int value, GameObject text)
{
    // Primer clic
    firstClickedButton = button;
    firstClickValue = value;
   
    if(text!=null) text.SetActive(true);
    else Debug.Log("Button null");
  
}

private void HandleSecondClick(Button button, int value, GameObject text)
{
    // Segundo clic
    secondClickedButton = button;
    secondClickValue = value;
      if(text!=null) text.SetActive(true);
    else Debug.Log("Button null");

    if (firstClickedButton.name == secondClickedButton.name)
    {
        Debug.Log("¡Mismo botón!");
    }
    else
    {
        CompareValues();
    }

    // Reiniciar variables para el siguiente par de clics
    firstClickedButton = null;
    secondClickedButton = null;
    firstClickValue = -1;
    secondClickValue = -1;
}

private void CompareValues()
{
    if (firstClickValue == secondClickValue)
    {
        Debug.Log("¡Valores iguales!");
        

     
        // Desactivar ambos botones clickeados
        firstClickedButton.interactable = false;
        secondClickedButton.interactable = false;
      
        gameResult.setPairs();
          gameResult.setScore(20);
    }
    else
    {
          TextMeshProUGUI buttonText = firstClickedButton.GetComponentInChildren<TextMeshProUGUI>();
           
         TextMeshProUGUI buttonTextTwo = secondClickedButton.GetComponentInChildren<TextMeshProUGUI>();
      
        StartCoroutine(waiterTime(0.5f, buttonText, buttonTextTwo));
        gameResult.setScore(-5);

         
    }
}



private IEnumerator waiterTime(float time,TextMeshProUGUI buttonText,TextMeshProUGUI buttonTextTwo){

       yield return new WaitForSeconds(time);
      
        buttonText.gameObject.SetActive(false);


    
        buttonTextTwo.gameObject.SetActive(false);
        Debug.Log("¡Valores diferentes!");



}

}
