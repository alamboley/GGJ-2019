using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public AudioManager audioManager;
    public WaveManager waveManager;
    public PlayerManager playerManager;

    public static Game instance;

    void Awake()
    {
        instance = this;
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
