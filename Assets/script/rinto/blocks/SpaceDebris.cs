using UnityEngine;

public class SpaceDebris : MonoBehaviour
{
    public float minSpeed = 0.1f;
    public float maxSpeed = 0.5f;
    public float directionChangeInterval = 2f; // ‰½•b‚²‚Æ‚ÉŒü‚«‚ð•Ï‚¦‚é‚©

    private Vector2 velocity;
    private float changeTimer;

    private void Start()
    {
        SetRandomVelocity();
        changeTimer = directionChangeInterval;
    }

    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime);

        // ŽžŠÔŒo‰ß‚Å•ûŒü‚ðƒ‰ƒ“ƒ_ƒ€‚É•Ï‚¦‚é
        changeTimer -= Time.deltaTime;
        if (changeTimer <= 0f)
        {
            SetRandomVelocity();
            changeTimer = directionChangeInterval;
        }

        // ‰æ–Ê’[‚Å’µ‚Ë•Ô‚é
        Vector2 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1) velocity.x *= -1;
        if (pos.y < 0 || pos.y > 1) velocity.y *= -1;
    }

    void SetRandomVelocity()
    {
        velocity = Random.insideUnitCircle.normalized * Random.Range(minSpeed, maxSpeed);
    }
}
