using UnityEngine;

public class ActivateAfterDelay : MonoBehaviour
{
    public GameObject targetObject;  // �A�N�e�B�u�ɂ������I�u�W�F�N�g
    public float delaySeconds = 3f;  // �ҋ@���ԁi�b�j

    void Start()
    {
        if (targetObject != null)
        {
            StartCoroutine(ActivateAfterTime());
        }
    }

    System.Collections.IEnumerator ActivateAfterTime()
    {
        yield return new WaitForSeconds(delaySeconds);
        targetObject.SetActive(true);
    }
}
