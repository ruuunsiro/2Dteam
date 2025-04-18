using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f; // ジャンプ力
    public Sprite[] walkSprites; // 足踏みスプライト2枚
    public Sprite idleSprite;    // 止まってるときのスプライト

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector3 defaultScale;

    private bool isGrounded; // 地面にいるかどうか
    private float animationTimer;
    private int currentWalkIndex;

    public Transform groundCheck; // 地面チェック用
    public LayerMask groundLayer; // 地面判定のレイヤーマスク

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultScale = transform.localScale;
    }

    void Update()
    {
        // 地面チェック（床との接触確認）
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // 横移動
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 向きの反転
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(defaultScale.x), defaultScale.y, defaultScale.z);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(defaultScale.x), defaultScale.y, defaultScale.z);
        }

        // ジャンプ処理
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // スペースキーでジャンプ
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // アニメーション処理
        if (Mathf.Abs(moveInput) > 0.01f && isGrounded)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer >= 0.2f) // アニメ切り替えの速度（秒）
            {
                animationTimer = 0f;
                currentWalkIndex = (currentWalkIndex + 1) % walkSprites.Length;
                spriteRenderer.sprite = walkSprites[currentWalkIndex];
            }
        }
        else if (isGrounded)
        {
            spriteRenderer.sprite = idleSprite;
            animationTimer = 0f;
            currentWalkIndex = 0;
        }
    }
}
