using UnityEngine;

public class FloatMovement : MonoBehaviour
{
    public float floatRange = 0.5f;  // ���V����͈�
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // �㉺�ɂӂ�ӂ킷��
        float yOffset = Mathf.Sin(Time.time) * floatRange;
        transform.position = new Vector3(transform.position.x, startPosition.y + yOffset, transform.position.z);
    }
}
