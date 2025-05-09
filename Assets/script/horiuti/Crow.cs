using UnityEngine;

public class Crow : MonoBehaviour
{
    public float speed = 5f;
    public bool moveLeft = true;

    void Update()
    {
        // �����E�Ɉړ�
        Vector2 direction = moveLeft ? Vector2.left : Vector2.right;
        transform.Translate(direction * speed * Time.deltaTime);

        // ��ʊO�ɏo����폜�ix���W�����ȏ�Ȃ�j
        if (transform.position.x < -15f || transform.position.x > 15f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
