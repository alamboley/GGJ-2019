using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyType
{
    C, 
    M,
    Y,
    K,
    W
}

[Serializable]
public class ColorConfig
{
    public EnemyType enemyType;
    public Color color;
}

public class Game : MonoBehaviour
{

    public static Game instance;

    [Header("REFS")]
    public AudioManager audioManager;
    public WaveManager waveManager;
    public PlayerManager playerManager;
    public CameraRotation camRotation;

    [Header("CONFIGS")]
    public List<ColorConfig> colorPalette = new List<ColorConfig>();


    public AnimationCurve playerProgCurve;
    public int minPlayers = 1;
    public int maxPlayers = 5;

    public AnimationCurve enemyProgCurve;
    public int minMonsters = 2;
    public int maxMonsters = 100;

    [Header("GAME")]
    public float gameDuration = 5f * 60f; // 60 seconds * 5
    [HideInInspector]
    public float gameTimeNormalized = 0f;


    //------
    float gameStartTime = 0f;

    void Awake()
    {
        instance = this;
        audioManager.OnBeat.AddListener(() =>
        {
            camRotation.Zoom();
        });
    }

    public ColorConfig GetColor(EnemyType enemyType)
    {
        foreach(ColorConfig cc in colorPalette)
        {
            if(cc.enemyType == enemyType)
            {
                return cc;
            }
        }

        Debug.LogError("NO COLOR CONFIG FOR ENEMYTYPE " + enemyType);
        return null;
    }

    private void Start()
    {
        this.gameStartTime = Time.unscaledTime;
    }

    private void Update()
    {
        this.gameTimeNormalized = (Time.unscaledTime-this.gameStartTime) / this.gameDuration;
        if (this.gameTimeNormalized >= 1.0f)
            this.gameTimeNormalized = 1.0f;

        if (this.gameTimeNormalized >= 1.0f)
            GameEnd();
    }

    public void HomeIsDead()
    {
        Reload();
    }

    void GameEnd()
    {
        Reload();
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
