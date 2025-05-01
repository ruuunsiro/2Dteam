using UnityEngine;

public class ActivateAfterDelay : MonoBehaviour
{
    public GameObject targetObject;  // アクティブにしたいオブジェクト
    public float delaySeconds = 3f;  // 待機時間（秒）

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
