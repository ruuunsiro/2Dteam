using UnityEngine;

public class Pufferfish : MonoBehaviour
{
    public float detectionRange = 3f;
    public GameObject spikes; // トゲの表示オブジェクト
    public Transform player;
    private bool isSpiked = false;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRange && !isSpiked)
        {
            isSpiked = true;
            spikes.SetActive(true);
        }
        else if (distance >= detectionRange && isSpiked)
        {
            isSpiked = false;
            spikes.SetActive(false);
        }

        Swim();
    }

    void Swim()
    {
        float wave = Mathf.Sin(Time.time * 2f) * 0.5f;
        transform.Translate(new Vector2(0.5f * Time.deltaTime, wave * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSpiked && collision.CompareTag("Player"))
        {
            // 「comeback」トリガーに当たったと同様の処理を行うには、
            // ここでプレイヤーに"comeback"タグを持つオブジェクトを踏ませるようにするか、
            // または、ここで直接Respawn関数を呼び出す設計にすることも可能です。

            // → 今回は「comeback」ゾーンに触れると死ぬという前提なので、
            // フグのトゲに「comeback」タグをつけておけばOKです（下記補足参照）
        }
    }
}
