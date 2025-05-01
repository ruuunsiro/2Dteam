using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerSpaceMovement : MonoBehaviour
{
    public float moveForce = 1.5f;        // 左右移動の推進力
    public float upwardForce = 1.2f;      // 上昇推進力（スペースキー）
    public float downwardDrift = -0.5f;   // 自然に下がる力（宇宙での緩やかな沈下）
    public float maxSpeed = 5f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.linearDamping = 0.1f;
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // 左右移動（慣性付き）
        if (Mathf.Abs(rb.linearVelocity.x) < maxSpeed)
        {
            rb.AddForce(Vector2.right * moveInput * moveForce);
        }

        // 自然に下がる
        rb.AddForce(Vector2.up * downwardDrift);

        // スペースキーで上昇
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * upwardForce);
        }
    }
}
