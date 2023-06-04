using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    public ScoreManager ScoreManager { get; private set; }
    public GameManager GameManager { get; private set; }
    
    public DragManager DragManager { get; private set; }
    public AbsorbablesManager AbsorbablesManager { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        ScoreManager = GetComponentInChildren<ScoreManager>();
        GameManager = GetComponentInChildren<GameManager>();
        DragManager = GetComponentInChildren<DragManager>();
        AbsorbablesManager = GetComponentInChildren<AbsorbablesManager>();
    }
}
