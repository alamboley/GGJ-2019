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
    
    void Start()
    {
        currentWave = 1;
    }
    
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
        Bounds bounds = OrthographicBounds();
        Vector2 randomAngle = Random.insideUnitCircle.normalized;
        float diagonal = Mathf.Sqrt((bounds.extents.x * bounds.extents.x) + (bounds.extents.y * bounds.extents.y)) / 2;

        Debug.Log(randomAngle * diagonal);

        return randomAngle;
        //return Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
    }

    static Bounds OrthographicBounds()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        
        return new Bounds(Camera.main.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
    }
}
