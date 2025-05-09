using UnityEngine;

public class AutoDestroyBullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 5f; // �����폜�܂ł̕b��

    private Vector2 direction;

    // �e�̔�ԕ������v���C���[�̈ʒu�Ɍ����ăZ�b�g
    public void SetDirection(Vector2 targetPosition)
    {
        direction = (targetPosition - (Vector2)transform.position).normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifetime); // ��莞�Ԍ�Ɏ����폜
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �v���C���[�ɓ��������ꍇ
        if (collision.gameObject.CompareTag("Player"))
        {
            // �v���C���[�̃��X�|�[���X�N���v�g���擾
            PlayerRespawn respawnScript = collision.gameObject.GetComponent<PlayerRespawn>();
            if (respawnScript != null)
            {
                respawnScript.Respawn(); // ���X�|�[�������s
            }

            Destroy(gameObject); // �e������
        }

        // �ǂɓ��������ꍇ
        if (collision.gameObject.CompareTag("SpaceBlock"))
        {
            Destroy(gameObject); // �e������
        }
    }
}
