using UnityEngine;

public class TrapFloor : MonoBehaviour
{
    public float fallSpeed = 50f;         // ���������鑬�x
    public float destroyDelay = 0.5f;     // �v���C���[�ڐG���牽�b��ɍ폜���邩

    private bool hasTriggered = false;    // ��x�ł��G�ꂽ���ǂ���
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
