using UnityEngine;

public class DestroyFloor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 0.1�b��ɂ��̏��I�u�W�F�N�g��j��
            Destroy(gameObject, 0.1f);
        }
    }
}
