using UnityEngine;

public class PlayerPipeExit : MonoBehaviour
{
    public float exitDuration = 1.5f;     // �t�F�[�h�A�E�g�ƈړ��ɂ����鍇�v���ԁi�b�j
    public float exitSpeed = 2f;          // �ړ����x�i���b�j

    private bool isExiting = false;
    private float exitTimer = 0f;

    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isExiting)
        {
            exitTimer += Time.deltaTime;
            float t = Mathf.Clamp01(exitTimer / exitDuration);

            // ���X�ɉE�ֈړ�
            transform.position += Vector3.right * exitSpeed * Time.deltaTime;

            // �t�F�[�h�A�E�g�i�A���t�@�����X��0�ցj
            float alpha = Mathf.Lerp(1f, 0f, t);
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);

            // �w�莞�Ԃ��߂������\����
            if (exitTimer >= exitDuration)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pipe") && !isExiting)
        {
            isExiting = true;

            // �Փ˖������������~
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.gravityScale = 0f;
                rb.isKinematic = true;
            }

            if (playerCollider != null)
                playerCollider.enabled = false;

            // ���̃v���C���[�X�N���v�g���~�i�����ȊO�j
            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                if (script != this)
                    script.enabled = false;
            }
        }
    }
}
