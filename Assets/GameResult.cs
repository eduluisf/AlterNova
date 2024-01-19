using UnityEngine;

[System.Serializable]
public class ResultsData
{
    public int total_clicks;
    public float total_time;
    public int pairs;
    public int score;
}

public class GameResult : MonoBehaviour
{
    [SerializeField] private GameTimer gameTimer;

    private int total_clicks = 0;
    private float total_time = 0;
    private int pairs = 0;
    private int score = 0;

    public void setTotalClicks()
    {
        total_clicks++;
        Debug.Log("Total clicks: " + total_clicks);
    }

    public void setPairs()
    {
        pairs++;
        Debug.Log("Total pairs: " + pairs);
        if (pairs == 6)
        {
            setTotalTime(gameTimer.GetGameTime());
        }
    }

    public void setScore(int scoreSended)
    {
        score+=scoreSended;;

        if(score<=0){score=0;}
        Debug.Log("Total score: " + score);
    }

    public void setTotalTime(float time)
    {
        total_time = time;
        Debug.Log("Total time: " + total_time);

        // Crear y guardar el JSON
        ResultsData resultsData = new ResultsData
        {
            total_clicks = total_clicks,
            total_time = total_time,
            pairs = pairs,
            score = score
        };

        string json = JsonUtility.ToJson(resultsData);
        Debug.Log(json);
        System.IO.File.WriteAllText("Assets/results.json", json);
        
    }
}
