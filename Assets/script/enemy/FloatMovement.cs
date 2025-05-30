using UnityEngine;

public class FloatMovement : MonoBehaviour
{
    public float floatRange = 0.5f;  // 浮遊する範囲
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // 上下にふわふわする
        float yOffset = Mathf.Sin(Time.time) * floatRange;
        transform.position = new Vector3(transform.position.x, startPosition.y + yOffset, transform.position.z);
    }
}
