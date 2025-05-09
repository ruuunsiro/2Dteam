using UnityEngine;

public class AutoDestroyBullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 5f; // 自動削除までの秒数

    private Vector2 direction;

    // 弾の飛ぶ方向をプレイヤーの位置に向けてセット
    public void SetDirection(Vector2 targetPosition)
    {
        direction = (targetPosition - (Vector2)transform.position).normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifetime); // 一定時間後に自動削除
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーに当たった場合
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーのリスポーンスクリプトを取得
            PlayerRespawn respawnScript = collision.gameObject.GetComponent<PlayerRespawn>();
            if (respawnScript != null)
            {
                respawnScript.Respawn(); // リスポーンを実行
            }

            Destroy(gameObject); // 弾を消去
        }

        // 壁に当たった場合
        if (collision.gameObject.CompareTag("SpaceBlock"))
        {
            Destroy(gameObject); // 弾を消去
        }
    }
}
