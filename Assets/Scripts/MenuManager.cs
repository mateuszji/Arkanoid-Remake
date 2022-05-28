using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text highScoreTMP;

    private int highScore;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreTMP.text = "HIGH SCORE: " + Environment.NewLine + highScore;    
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitApp()
    {
        Application.Quit();
    }
}
