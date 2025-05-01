using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;

    private Vector3 originalPos;
    private float currentDuration = 0f;

    private void Awake()
    {
        originalPos = transform.localPosition;
    }

    private void Update()
    {
        if (currentDuration > 0)
        {
            Vector2 shakeOffset = Random.insideUnitCircle * shakeMagnitude;
            transform.localPosition = originalPos + new Vector3(shakeOffset.x, shakeOffset.y, 0);
            currentDuration -= Time.deltaTime;

            if (currentDuration <= 0f)
            {
                transform.localPosition = originalPos;
            }
        }
    }

    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        currentDuration = duration;
    }
}
