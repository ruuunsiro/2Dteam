using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerSpaceMovement : MonoBehaviour
{
    public float thrustForce = 1.5f;
    public float rotateSpeed = 180f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // ���d�́I
        rb.linearDamping = 0.1f;       // ������Ƃ�����R�i����OK�j
    }

    private void Update()
    {
        float rotateInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, -rotateInput * rotateSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Vector2 forceDir = transform.up; // �v���C���[�̌����Ă����
            rb.AddForce(forceDir * thrustForce);
        }
    }
}
