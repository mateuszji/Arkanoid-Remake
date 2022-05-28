using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;
    public static GameManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

    public bool isPlayingNow { get; set; }
    public int currentLevel;

    public int availableLives = 3;
    private int score = 0;
    public int lives { get; set; }

    [SerializeField]
    private TMP_Text livesCountTMP;
    [SerializeField]
    private TMP_Text scoreTMP;
    [SerializeField]
    private TMP_Text currentLevelTMP;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject winScreen;

    private void Start()
    {
        this.lives = availableLives;
        livesCountTMP.text = "LIVES: " + Environment.NewLine + lives;
        currentLevelTMP.text = currentLevel.ToString();
        Ball.onBallDestroy += onBallDestroy;
        Brick.onBrickDestroy += onBrickDestroy;
    }

    private void onBrickDestroy(Brick brick)
    {
        score += 10;
        scoreTMP.text = "SCORE: " + Environment.NewLine + score.ToString().PadLeft(6, '0');

        if (BricksManager.Instance.remainingBricks.Count <= 0)
        {
            BallsManager.Instance.resetBalls();
            isPlayingNow = false;
            currentLevel++;
            if (currentLevel >= BricksManager.Instance.levelsData.Count)
                showWinScreen();
            else
            {
                BricksManager.Instance.loadLevel(currentLevel);
                currentLevelTMP.text = currentLevel.ToString();
            }
        }
    }
    private void onBallDestroy(Ball ball)
    {
        if (BallsManager.Instance.Balls.Count <= 0)
        {
            lives--;
            livesCountTMP.text = "LIVES: " + Environment.NewLine + lives;
            if (lives <= 0)
            {
                gameOverScreen.SetActive(true);
                UpdateHighScore(score);
                gameOverScreen.transform.Find("ScoreText").GetComponent<TMP_Text>().text = "YOUR SCORE: " + Environment.NewLine + score.ToString().PadLeft(6, '0');
                gameOverScreen.transform.Find("HighScoreText").GetComponent<TMP_Text>().text = "HIGH SCORE: " + Environment.NewLine + PlayerPrefs.GetInt("HighScore").ToString().PadLeft(6, '0');
            }
            else
            {
                BallsManager.Instance.resetBalls();
                isPlayingNow = false;
                BricksManager.Instance.loadLevel(currentLevel);
            }
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void showWinScreen()
    {
        winScreen.SetActive(true);
        UpdateHighScore(score);
        winScreen.transform.Find("ScoreText").GetComponent<TMP_Text>().text = "YOUR SCORE: " + Environment.NewLine + score.ToString().PadLeft(6, '0');
        winScreen.transform.Find("HighScoreText").GetComponent<TMP_Text>().text = "HIGH SCORE: " + Environment.NewLine + PlayerPrefs.GetInt("HighScore").ToString().PadLeft(6, '0');
    }

    private void UpdateHighScore(int score)
    {
        int oldHighScore = PlayerPrefs.GetInt("HighScore");
        if (score > oldHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    private void OnDisable()
    {
        Ball.onBallDestroy -= onBallDestroy;
        Brick.onBrickDestroy -= onBrickDestroy;
    }
}
