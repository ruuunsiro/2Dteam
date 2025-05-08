using UnityEngine;

public class DestroyFloor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 0.1秒後にこの床オブジェクトを破壊
            Destroy(gameObject, 0.1f);
        }
    }
}
