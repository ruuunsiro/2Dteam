using UnityEngine;

public class TrapFloor : MonoBehaviour
{
    public float fallSpeed = 50f;         // 床が落ちる速度
    public float destroyDelay = 0.5f;     // プレイヤー接触から何秒後に削除するか

    private bool hasTriggered = false;    // 一度でも触れたかどうか
    private bool shouldFall = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!hasTriggered && collision.gameObject.CompareTag("Player"))
        {
            hasTriggered = true;
            shouldFall = true;
            Invoke(nameof(DestroyFloor), destroyDelay);
        }
    }

    void Update()
    {
        if (shouldFall)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
    }

    void DestroyFloor()
    {
        Destroy(gameObject);
    }
}
