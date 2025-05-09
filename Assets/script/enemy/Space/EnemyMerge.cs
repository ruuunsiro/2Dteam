using UnityEngine;

public class EnemyMerge : MonoBehaviour
{
    public GameObject partner;                  // 合体する相手（インスペクターで指定）
    public float mergeDelay = 10f;              // 何秒後に合体するか
    public float moveSpeed = 2f;                // 合体に向かう移動速度
    public GameObject mergedObjectPrefab;       // 合体後に生成されるオブジェクト

    private float timer = 0f;
    private bool isMerging = false;

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 合体処理開始条件
        if (!isMerging && timer >= mergeDelay && partner != null)
        {
            isMerging = true;

            // 浮遊を停止（オプション）
            var myFloat = GetComponent<FloatMovement>();
            if (myFloat != null) myFloat.enabled = false;

            var partnerFloat = partner.GetComponent<FloatMovement>();
            if (partnerFloat != null) partnerFloat.enabled = false;

            // 他のスクリプト（例: SpaceEnemyShooter）も止める
            var myShooter = GetComponent<SpaceEnemyShooter>();
            if (myShooter != null) myShooter.enabled = false;

            var partnerShooter = partner.GetComponent<SpaceEnemyShooter>();
            if (partnerShooter != null) partnerShooter.enabled = false;
        }

        // 合体中の移動処理
        if (isMerging && partner != null)
        {
            // 互いに中心に向かって移動（片方向でもOK）
            transform.position = Vector3.MoveTowards(transform.position, partner.transform.position, moveSpeed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, partner.transform.position);
            if (distance < 0.1f)
            {
                // 合体完了
                Vector3 center = (transform.position + partner.transform.position) / 2f;
                Instantiate(mergedObjectPrefab, center, Quaternion.identity);
                Destroy(partner);
                Destroy(gameObject);
            }
        }
        if (isMerging)
        {
            var myChase = GetComponent<ChasePlayer>();
            if (myChase != null) myChase.enabled = false;

            if (partner != null)
            {
                var partnerChase = partner.GetComponent<ChasePlayer>();
                if (partnerChase != null) partnerChase.enabled = false;
            }
        }

    }

}
