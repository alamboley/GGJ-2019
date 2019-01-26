using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 1;
    public int monstersKilledCount = 0;
    public float waveDuration = 7.5f;
    public float ratioSpawn = 1.9f;

    public Enemy EnemyPrefab;
    public Transform RootSpawner;
    public Text WaveText;
    
    float diagonal;

    /// <summary>
    /// Start function called at the beginning of the project
    /// </summary>
    void Start()
    {
        currentWave = 1;

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        diagonal = Mathf.Sqrt((height * height) + (width * width)) / 2;

        UpdateWaveText();

        StartCoroutine(UpdateMonsters());
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
    public void MonsterKilled()
    {
        monstersKilledCount++;
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
            int monsterCount = (int)(ratioSpawn * 2);

            // Instantiate a new monster with a little delay
            for (int i = 0; i < monsterCount; i++)
            {
                Instantiate(EnemyPrefab, GetRandomPosition(), Quaternion.identity, RootSpawner);
                yield return new WaitForSeconds(0.8f);
            }

            yield return new WaitForSeconds(waveDuration);
        }
    }
}
