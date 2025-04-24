using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float lowJumpMultiplier = 2f;
    public float fallMultiplier = 3f;

    public Sprite[] walkSprites;
    public Sprite idleSprite;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector3 defaultScale;

    private float animationTimer;
    private int currentWalkIndex;

    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultScale = transform.localScale;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 向きの反転
        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(defaultScale.x), defaultScale.y, defaultScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(defaultScale.x), defaultScale.y, defaultScale.z);

        // ジャンプ（1回押し）
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // ジャンプ慣性：ふんわり or 加速落下
        if (rb.linearVelocity.y < 0)
        {
            // 落下時：落下加速
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            // 上昇中にスペースを離したらふわっと
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // 歩きアニメーション
        if (Mathf.Abs(moveInput) > 0.01f)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer >= 0.2f)
            {
                animationTimer = 0f;
                currentWalkIndex = (currentWalkIndex + 1) % walkSprites.Length;
                spriteRenderer.sprite = walkSprites[currentWalkIndex];
            }
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
            animationTimer = 0f;
            currentWalkIndex = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    public void DecreaseJumpForce(float amount)
    {
        jumpForce = Mathf.Max(0f, jumpForce - amount);
    }
}
