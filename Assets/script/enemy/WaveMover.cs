using UnityEngine;

public class WaveMover : MonoBehaviour
{
    public float moveSpeed = 2f;  // 左に動く速さ
    public float pushForce = 5f;  // プレイヤーへの押し力

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // 画面外に出たら削除
        if (transform.position.x < Camera.main.ViewportToWorldPoint(Vector3.zero).x - 2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.left * pushForce);
            }
        }
    }
}
