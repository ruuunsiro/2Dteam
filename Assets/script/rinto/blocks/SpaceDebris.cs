using UnityEngine;

public class SpaceDebris : MonoBehaviour
{
    public float minSpeed = 0.1f;
    public float maxSpeed = 0.5f;
    public float directionChangeInterval = 2f; // ���b���ƂɌ�����ς��邩

    private Vector2 velocity;
    private float changeTimer;

    private void Start()
    {
        SetRandomVelocity();
        changeTimer = directionChangeInterval;
    }

    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime);

        // ���Ԍo�߂ŕ����������_���ɕς���
        changeTimer -= Time.deltaTime;
        if (changeTimer <= 0f)
        {
            SetRandomVelocity();
            changeTimer = directionChangeInterval;
        }

        // ��ʒ[�Œ��˕Ԃ�
        Vector2 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1) velocity.x *= -1;
        if (pos.y < 0 || pos.y > 1) velocity.y *= -1;
    }

    void SetRandomVelocity()
    {
        velocity = Random.insideUnitCircle.normalized * Random.Range(minSpeed, maxSpeed);
    }
}
