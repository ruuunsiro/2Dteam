using UnityEngine;

public class JellyfishMovement : MonoBehaviour
{
    public float floatSpeed = 1f;     // 上下の速さ
    public float driftSpeed = 0.5f;   // 水平方向にゆっくり漂うスピード
    public float driftChangeInterval = 2f; // 漂う方向を変える間隔

    public float rotationSpeed = 20f;

    private Vector3 startPos;
    private float driftTimer = 0f;
    private Vector2 driftDirection;
    private Camera mainCam;
    private float spriteHalfWidth;

    // 海流の変動度合い
    private float driftVariance = 0.3f;

    void Start()
    {
        startPos = transform.position;
        mainCam = Camera.main;

        // SpriteRendererからスプライトの幅を取得
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            spriteHalfWidth = sr.bounds.size.x / 2f;
        }

        ChangeDriftDirection();
    }

    void Update()
    {
        // ドリフト移動（ランダム変動+流れの変化）
        driftTimer += Time.deltaTime;
        if (driftTimer >= driftChangeInterval)
        {
            ChangeDriftDirection();
            driftTimer = 0f;
        }

        // ドリフトに小さな変動を追加（自然な流れを再現）
        Vector3 driftMovement = new Vector3(driftDirection.x + Random.Range(-driftVariance, driftVariance), 0, 0) * driftSpeed * Time.deltaTime;

        // 障害物との衝突判定（レイキャスト）
        RaycastHit2D hit = Physics2D.Raycast(transform.position, driftDirection, driftSpeed * Time.deltaTime);

        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            // 障害物に当たったら反対方向に移動
            driftDirection = -driftDirection;
        }

        // 画面端に出ないようにX軸方向を制限
        Vector3 newPosition = transform.position + driftMovement;
        newPosition.x = Mathf.Clamp(newPosition.x, GetScreenBounds().x + spriteHalfWidth, GetScreenBounds().y - spriteHalfWidth);

        // 画面外に出たらオブジェクトを削除
        if (newPosition.y < mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCam.nearClipPlane)).y)
        {
            Destroy(gameObject);  // オブジェクトを削除
            return;
        }

        transform.position = newPosition;

        // 回転
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void ChangeDriftDirection()
    {
        // 海流の方向をランダムに変える
        driftDirection = new Vector2(Random.Range(-1f, 1f), 0).normalized;
    }

    // 画面の左右端の位置を取得
    private Vector2 GetScreenBounds()
    {
        Vector3 bottomLeft = mainCam.ViewportToWorldPoint(new Vector3(0, 0, mainCam.nearClipPlane));
        Vector3 topRight = mainCam.ViewportToWorldPoint(new Vector3(1, 1, mainCam.nearClipPlane));

        return new Vector2(bottomLeft.x, topRight.x);
    }
}
