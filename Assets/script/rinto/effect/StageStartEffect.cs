using UnityEngine;

public class StageStartEffect : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;

    private void Start()
    {
        CameraShake cameraShake = GetComponent<CameraShake>();
        if (cameraShake != null)
        {
            cameraShake.TriggerShake(shakeDuration, shakeMagnitude);
        }
    }
}
