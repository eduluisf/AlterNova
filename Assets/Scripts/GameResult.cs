using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Results
{
    public int total_clicks;
    public float total_time;
    public int pairs;
    public int score;
}

[System.Serializable]
public class ResultsWrapper
{
    public Results results;
}

public class GameResult : MonoBehaviour
{
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private JsonReader jsonReader;
    [SerializeField] private GameObject textObj;
    [SerializeField] private TextMeshProUGUI[] buttonText;
    public AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private int total_clicks = 0;
    private float total_time = 0;
    private int pairs = 0;
    private int score = 0;
    private int pairMatrix = 0;

    private void Start()
    {
        StartCoroutine(waitNumber());
    }

    private IEnumerator waitNumber()
    {
        yield return new WaitUntil(() => jsonReader.GetPair() != 0);
        pairMatrix = jsonReader.GetPair();
        Debug.Log($"Desde OtroScript - El valor el paires: {pairMatrix}");
    }

    public void setTotalClicks()
    {
        total_clicks++;
        buttonText[2].text = total_clicks.ToString();
        Debug.Log("Total clicks: " + total_clicks);
    }

    public void setPairs()
    {
        pairs++;
        buttonText[1].text = pairs.ToString();
        Debug.Log("Total pairs: " + pairs);
        if (pairs == pairMatrix)
        {
            gameTimer.StopTime();
            setTotalTime(gameTimer.GetGameTime());

            audioSource.PlayOneShot(audioClip);

            textObj.SetActive(true);
        }
    }

    public void setScore(int scoreSended)
    {
        score += scoreSended;

        if (score <= 0)
        {
            score = 0;
            buttonText[0].text = score.ToString();
        }
        else
        {
            buttonText[0].text = score.ToString();
        }

        Debug.Log("Total score: " + score);
    }

    public void setTotalTime(float time)
    {
        total_time = time;

        Debug.Log("Total time: " + total_time);

        // Crear y guardar el JSON
        Results resultsData = new Results
        {
            total_clicks = total_clicks,
            total_time = total_time,
            pairs = pairs,
            score = score
        };

        ResultsWrapper resultsWrapper = new ResultsWrapper
        {
            results = resultsData
        };

        string json = JsonUtility.ToJson(resultsWrapper);
        Debug.Log(json);
        System.IO.File.WriteAllText("Assets/results.json", json);
    }
}
