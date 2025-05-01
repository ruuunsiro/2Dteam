using UnityEngine;

public class JellyfishMovement : MonoBehaviour
{
    public float floatSpeed = 1f;     // 上下の速さ
    public float floatHeight = 0.5f;  // 上下の幅
    public float driftSpeed = 0.5f;   // 水平方向にゆっくり漂うスピード
    public float driftChangeInterval = 2f; // 漂う方向を変える間隔

    public float rotationSpeed = 20f;

    private Vector3 startPos;
    private float driftTimer = 0f;
    private Vector2 driftDirection;

    void Start()
    {
        startPos = transform.position;
        ChangeDriftDirection();
    }

    void Update()
    {
        // 上下フワフワ（サイン波）
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // ドリフト移動（少しずつランダムな方向へ）
        driftTimer += Time.deltaTime;
        if (driftTimer >= driftChangeInterval)
        {
            ChangeDriftDirection();
            driftTimer = 0f;
        }

        Vector3 driftMovement = new Vector3(driftDirection.x, 0, 0) * driftSpeed * Time.deltaTime;
        transform.position += driftMovement;

        // 高さを更新（ドリフト後にY座標だけ固定）
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

    }

    void ChangeDriftDirection()
    {
        // -1〜1の間でランダムなX方向
        driftDirection = new Vector2(Random.Range(-1f, 1f), 0).normalized;
    }
}
