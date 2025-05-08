using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject wavePrefab;
    public float spawnInterval = 5f;  // 波が出現する間隔（秒）
    public Vector2 spawnPosition = new Vector2(10f, 0f); // 出現位置（右側）

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnWave();
            timer = 0f;
        }
    }

    void SpawnWave()
    {
        Instantiate(wavePrefab, spawnPosition, Quaternion.identity);
    }
}
