using UnityEngine;

public class SquidMovement : MonoBehaviour
{
    public float moveSpeed = 1.5f;            // �ړ����x
    public float moveRange = 3f;              // �ړ��͈́i���E�j
    public float verticalBobAmount = 0.5f;    // �c�ɂӂ�ӂ�h��镝
    public float bobSpeed = 2f;               // �c�h��̃X�s�[�h

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
        // ���E�ړ�
        float direction = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // �͈͊O�ɏo���������ς���
        if (Mathf.Abs(transform.position.x - startPos.x) > moveRange)
        {
            movingRight = !movingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // ���]
            transform.localScale = localScale;
        }

        // �㉺�ӂ�ӂ�ړ�
        float bob = Mathf.Sin(Time.time * bobSpeed) * verticalBobAmount;
        transform.position = new Vector3(transform.position.x, startPos.y + bob, transform.position.z);
    }
}
