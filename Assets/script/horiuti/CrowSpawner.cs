using UnityEngine;

public class CrowSpawner : MonoBehaviour
{
    public GameObject crowPrefab;
    public float spawnInterval = 2f;
    public float xRange = 8f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnCrow), 1f, spawnInterval);
    }

    void SpawnCrow()
    {
        float xPos = Random.Range(-xRange, xRange);
        Vector3 spawnPos = new Vector3(xPos, 6f, 0f);
        Instantiate(crowPrefab, spawnPos, Quaternion.identity);
    }
}
