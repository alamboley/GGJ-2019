using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 1;
    public int monstersKilledCount = 0;

    public float gameStartTime = 0f;
    public float waveDuration = 7.5f;
    public float ratioSpawn = 1.9f;
    private float previousCurveValue;

    public List<Enemy> enemies;

    public Enemy EnemyPrefab;
    public Transform RootSpawner;
    public Text WaveText;

    
    float diagonal;

    EnemiesPoolManager EnemiesPoolManager;

    /// <summary>
    /// Start function called at the beginning of the project
    /// </summary>
    void Start()
    {
        EnemiesPoolManager = FindObjectOfType<EnemiesPoolManager>();

        currentWave = 1;

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        diagonal = Mathf.Sqrt((height * height) + (width * width)) / 2;

        UpdateWaveText();

        gameStartTime = Time.unscaledTime;

        StartCoroutine(UpdateMonsters());
    }

    void Awake()
    {
        enemies = new List<Enemy>();
    }
    
    /// <summary>
    /// Update function called every frame
    /// </summary>
    void Update()
    {
        // Decrease time between each monster spawn
        if(monstersKilledCount >= (currentWave + 1) * ratioSpawn)
        {
            currentWave++;
            waveDuration -= 0.1f;
        }

        UpdateWaveText();
    }

    /// <summary>
    /// UI Text update
    /// </summary>
    void UpdateWaveText()
    {
        WaveText.text = "Vague : " + currentWave;
    }

    /// <summary>
    /// Incremente the monsters killed count
    /// </summary>
    public void MonsterKilled(Enemy enemy)
    {
        monstersKilledCount++;
        enemies.Remove(enemy);
    }

    /// <summary>
    /// Get a random spawning position for monsters
    /// </summary>
    /// <returns></returns>
    Vector3 GetRandomPosition()
    {
        Vector2 randomAngle = Random.insideUnitCircle.normalized;

        return randomAngle * diagonal;
    }
    
    /// <summary>
    /// Coroutine called every wave duration to Spawn some monsters
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateMonsters()
    {
        while(true)
        {
            float curvevalue = Game.instance.enemyProgCurve.Evaluate(Game.instance.gameTimeNormalized);
            int enemiesRequired = Mathf.RoundToInt(Mathf.Lerp(Game.instance.minMonsters,Game.instance.maxMonsters,curvevalue));
            for (int i = enemies.Count; i < enemiesRequired; i++)
            {
                Enemy enemy = EnemiesPoolManager.GetFirstInactiveEnemy();
                enemy.transform.position = GetRandomPosition();
                enemy.transform.rotation = Quaternion.identity;

                enemy.ResetEnemy();

                enemy.gameObject.SetActive(true);
                
                yield return new WaitForSeconds(0.8f);
            }

            yield return new WaitForSeconds(waveDuration);
        }
    }
}
