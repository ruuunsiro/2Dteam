using UnityEngine;

public class SquidMovement : MonoBehaviour
{
    public float moveSpeed = 1.5f;            // 移動速度
    public float moveRange = 3f;              // 移動範囲（左右）
    public float verticalBobAmount = 0.5f;    // 縦にふよふよ揺れる幅
    public float bobSpeed = 2f;               // 縦揺れのスピード

    private Vector3 startPos;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 左右移動
        float direction = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // 範囲外に出たら向きを変える
        if (Mathf.Abs(transform.position.x - startPos.x) > moveRange)
        {
            movingRight = !movingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // 反転
            transform.localScale = localScale;
        }

        // 上下ふよふよ移動
        float bob = Mathf.Sin(Time.time * bobSpeed) * verticalBobAmount;
        transform.position = new Vector3(transform.position.x, startPos.y + bob, transform.position.z);
    }
}
