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

    float time = 0;
    float diagonal;


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

    void UpdateWaveText()
    {
        WaveText.text = "Vague : " + currentWave;
    }

    public void MonsterKilled()
    {
        monstersKilledCount++;
    }

    void SpawnMonsters()
    {
        int monsterCount = (int)(ratioSpawn * 2);

        for(int i = 0; i < monsterCount; i++)
        {
            Instantiate(EnemyPrefab, GetRandomPosition(), Quaternion.identity, RootSpawner);
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector2 randomAngle = Random.insideUnitCircle.normalized;

        return randomAngle * diagonal;
    }

    IEnumerator UpdateMonsters()
    {
        while(true)
        {
            SpawnMonsters();
            yield return new WaitForSeconds(waveDuration);
        }
    }
}
