using UnityEngine;

public class PlayerUnderwaterMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float waterDrag = 2f;
    public float floatStrength = 5f;
    public float weight = 0f; // 重さによって浮力に影響

    public Sprite[] swimSprites;
    public Sprite idleSprite;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector3 defaultScale;

    private float animationTimer;
    private int currentSwimIndex;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultScale = transform.localScale;

        rb.gravityScale = 0.5f;
        rb.linearDamping = waterDrag;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector2 inputDir = new Vector2(moveX, 0f).normalized;

        rb.AddForce(inputDir * moveSpeed);

        // スペースキーで浮上（重さがあるほど浮きにくい）
        if (Input.GetKey(KeyCode.Space))
        {
            float adjustedFloat = floatStrength - weight;
            rb.AddForce(Vector2.up * Mathf.Max(0f, adjustedFloat));
        }

        // 向きの反転
        if (moveX > 0)
            transform.localScale = new Vector3(Mathf.Abs(defaultScale.x), defaultScale.y, defaultScale.z);
        else if (moveX < 0)
            transform.localScale = new Vector3(-Mathf.Abs(defaultScale.x), defaultScale.y, defaultScale.z);

        // 泳ぎアニメーション
        if (Mathf.Abs(moveX) > 0.01f)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer >= 0.2f)
            {
                animationTimer = 0f;
                currentSwimIndex = (currentSwimIndex + 1) % swimSprites.Length;
                spriteRenderer.sprite = swimSprites[currentSwimIndex];
            }
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
            animationTimer = 0f;
            currentSwimIndex = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            weight += 0.3f; // 重さを追加して浮上しにくくする
            Destroy(other.gameObject);
        }
    }
}
