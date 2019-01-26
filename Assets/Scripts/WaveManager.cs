using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 1;
    public int monsterCount = 1;
    public int waveDuration = 10;
    public float ratioSpawn = 1.9f;

    public GameObject monster;
    public GameObject spawn;

    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - time > waveDuration)
        {
            currentWave++;
            time = Time.time;
            SpawnMonsters();
        }
    }

    void SpawnMonsters()
    {
        Vector3 position = GetRandomPosition();
        Instantiate(monster, position, Quaternion.identity);
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawn.transform.localPosition.x, spawn.transform.localScale.x);
        float y = Random.Range(spawn.transform.localPosition.y, spawn.transform.localScale.y);

        return new Vector3(x, y, 0);
    }
}
