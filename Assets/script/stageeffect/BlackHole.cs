using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float maxPullStrength = 20f;
    public float minPullStrength = 1f;
    public float eventHorizon = 0.5f;  // ブラックホールの消失範囲
    public float activationDelay = 3f;
    public float effectiveRadius = 5f;
    public float rotationTorque = 5f;

    private bool isActive = false;

    void Start()
    {
        Invoke(nameof(ActivateBlackHole), activationDelay);
    }

    void ActivateBlackHole()
    {
        isActive = true;
        Debug.Log("ブラックホール起動！");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!isActive) return;

        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        // キャラクターを消さないようにタグをチェック
        bool isPlayer = other.CompareTag("Player");

        // 吸い込み力の適用（キャラクターにも吸い込む力を適用）
        float distance = Vector2.Distance(transform.position, other.transform.position);
        if (distance > effectiveRadius) return;

        Vector2 direction = (transform.position - other.transform.position).normalized;

        float t = 1 - Mathf.Clamp01(distance / effectiveRadius);
        float pullForce = Mathf.Lerp(minPullStrength, maxPullStrength, t);

        rb.AddForce(direction * pullForce);

        // 回転を追加（中心に近いほど速く回す）
        float torque = rotationTorque * t;
        rb.AddTorque(torque);

        // キャラクター以外のオブジェクトだけ消す
        if (!isPlayer && distance < eventHorizon)
        {
            Destroy(other.gameObject);  // キャラクター以外を消す
        }
    }
}
