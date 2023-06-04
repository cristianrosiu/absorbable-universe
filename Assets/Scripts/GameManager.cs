using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private bool _isPaused;

    public UnityEvent<bool> onPause;
    public UnityEvent onRestart;
    public UnityEvent onGameOver;

    [SerializeField] private BlackHole blackHole;
    [SerializeField] private float defaultBlackHoleMass = 5f;
    [SerializeField] private Sprite[] bgSprites;
    [SerializeField] private SpriteRenderer bgSpriteRenderer;

    private bool _gameOver;

    private void Awake()
    {
        bgSpriteRenderer.sprite = bgSprites[0];
    }

    private void Update()
    {
        if (!_gameOver && blackHole.M <= 0)
            OnGameOver();
        
        if (Input.GetKeyDown(KeyCode.Escape) && !_gameOver)
        {
            if (_isPaused)
            {
                _isPaused = false;
                Time.timeScale = 1.0f;
            }
            else
            {
                _isPaused = true;
                Time.timeScale = 0.0f;
            }
            onPause?.Invoke(_isPaused);
        }
    }

    public void OnGameOver()
    {
        onGameOver?.Invoke();
        
        _gameOver = true;
        _isPaused = true;
        Time.timeScale = 0.0f;

        bgSpriteRenderer.sprite = bgSprites[1];
        
        Singleton.Instance.AbsorbablesManager.RemoveAllObjects();
        
        Singleton.Instance.DragManager.enabled = false;
        Singleton.Instance.AbsorbablesManager.enabled = false;
    }

    public void Restart()
    {
        _gameOver = false;
        _isPaused = false;
        Time.timeScale = 1.0f;
        
        bgSpriteRenderer.sprite = bgSprites[0];
        
        onRestart?.Invoke();
        
        Singleton.Instance.DragManager.enabled = true;
        Singleton.Instance.AbsorbablesManager.enabled = true;
        blackHole.M = defaultBlackHoleMass;
    }
}
