using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BestScoreText;
    public int Score;
    public int BestScore;

    private void Start()
    {
        BestScore = PlayerPrefs.GetInt("BestScore");
        Score = PlayerPrefs.GetInt("Score");
        ScoreText.text = "YOUR SCORE " + Score;
        BestScoreText.text = "BEST SCORE " + BestScore;
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
