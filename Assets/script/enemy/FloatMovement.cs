using UnityEngine;

public class FloatMovement : MonoBehaviour
{
    public float floatRange = 0.5f;  // ïÇóVÇ∑ÇÈîÕàÕ
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // è„â∫Ç…Ç”ÇÌÇ”ÇÌÇ∑ÇÈ
        float yOffset = Mathf.Sin(Time.time) * floatRange;
        transform.position = new Vector3(transform.position.x, startPosition.y + yOffset, transform.position.z);
    }
}
