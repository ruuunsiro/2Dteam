using UnityEngine;

public class Crow : MonoBehaviour
{
    public float speed = 5f;
    public bool moveLeft = true;

    void Update()
    {
        // 左か右に移動
        Vector2 direction = moveLeft ? Vector2.left : Vector2.right;
        transform.Translate(direction * speed * Time.deltaTime);

        // 画面外に出たら削除（x座標が一定以上なら）
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
