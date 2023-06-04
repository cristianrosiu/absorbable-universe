using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int highscore = 5;
    public int score = 5;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text endScoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private GameObject endPanel;
    
    private void Awake()
    {
        UpdateScoreText();
    }

    private void OnEnable()
    {
        Singleton.Instance.GameManager.onPause.AddListener(OnGamePaused);
        Singleton.Instance.GameManager.onRestart.AddListener(OnGameRestart);
        Singleton.Instance.GameManager.onGameOver.AddListener(OnGameOver);
    }

    private void OnDisable()
    {
        Singleton.Instance.GameManager.onPause.RemoveListener(OnGamePaused);
        Singleton.Instance.GameManager.onRestart.AddListener(OnGameRestart);
        Singleton.Instance.GameManager.onGameOver.RemoveListener(OnGameOver);
    }
    
    private void OnGamePaused(bool isPaused)
    {
        endPanel.SetActive(isPaused);
    }

    private void OnGameOver()
    {
        endPanel.SetActive(true);
    }
    private void OnGameRestart()
    {
        endPanel.SetActive(false);
        score = 5;
        UpdateScoreText();
    }

    public void ChangeScore(int value)
    {
        score += value;
        
        if (highscore < score)
            highscore = score;
        
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "SOLAR MASS: " + score;
        endScoreText.text = scoreText.text;
        highscoreText.text = "HIGHSCORE: " + highscore;
    }
}
