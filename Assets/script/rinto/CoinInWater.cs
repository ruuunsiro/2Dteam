using UnityEngine;

public class CoinInWater : MonoBehaviour
{
    public float initialFallSpeed = 5f;
    public float slowDownDuration = 2f;
    public float minFallSpeed = 0.3f;

    private float fallTimer = 0f;
    private bool isStopped = false;

    void Update()
    {
        if (isStopped) return;

        fallTimer += Time.deltaTime;
        float t = Mathf.Clamp01(fallTimer / slowDownDuration);
        float currentFallSpeed = Mathf.Lerp(initialFallSpeed, minFallSpeed, t);

        transform.position += Vector3.down * currentFallSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            isStopped = true;
        }
    }
}
