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

    [Header("CONFIGS")]
    public List<ColorConfig> colorPalette = new List<ColorConfig>();


    void Awake()
    {
        instance = this;
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

    }

    public void HomeIsDead()
    {
        Reload();
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
