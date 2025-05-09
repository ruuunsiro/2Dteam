using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BlackHole : MonoBehaviour
{
    public float maxPullStrength = 20f;  // 最大引き寄せ力
    public float minPullStrength = 1f;   // 最小引き寄せ力
    public float eventHorizon = 0.5f;    // ブラックホールの消失範囲
    public float rotationTorque = 5f;    // 回転の力

    private CircleCollider2D circleCollider;
    private ContactFilter2D filter;
    private Collider2D[] results = new Collider2D[50];
    private bool isActive = true;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider == null)
        {
            Debug.LogWarning("BlackHole に CircleCollider2D がありません。");
        }

        filter = new ContactFilter2D();
        filter.useTriggers = true;
        filter.NoFilter(); // すべてを対象にする
    }

    void Update()
    {

        if (!isActive || circleCollider == null) return;

        int count = circleCollider.Overlap(filter, results);

        for (int i = 0; i < count; i++)
        {
            Collider2D target = results[i];
            
            // 自分自身は除外
            //if (target == circleCollider) continue;

            Rigidbody2D rb = target.attachedRigidbody;
            if (rb == null) continue;

            Vector2 direction = (transform.position - target.transform.position).normalized;
            float distance = Vector2.Distance(transform.position, target.transform.position);

            // 距離に応じて吸引力と回転力を調整
            float t = 1 - Mathf.Clamp01(distance / (circleCollider.radius * transform.localScale.x));
            float pullForce = Mathf.Lerp(minPullStrength, maxPullStrength, t);
            rb.AddForce(direction * pullForce);
            rb.AddTorque(rotationTorque * t);

            // イベントホライズン内に入ったオブジェクト（Player以外）は消す
            if (distance < eventHorizon && !target.CompareTag("Player"))
            {
                Destroy(target.gameObject);
            }
        }
    }

}
