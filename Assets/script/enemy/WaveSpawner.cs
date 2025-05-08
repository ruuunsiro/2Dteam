using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject wavePrefab;
    public float spawnInterval = 5f;  // �g���o������Ԋu�i�b�j
    public Vector2 spawnPosition = new Vector2(10f, 0f); // �o���ʒu�i�E���j

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
