using UnityEngine;

public class SpaceDebris : MonoBehaviour
{
    public float minSpeed = 0.1f;
    public float maxSpeed = 0.5f;
    public float directionChangeInterval = 2f;
    public float boostSpeed = 2f;            // �Փˎ��̉���
    public float boostDuration = 1.5f;       // �����̎�������
    public float rotationBoost = 180f;       // �Փˎ��̉�]�̋���

    private Vector2 velocity;
    private float changeTimer;
    private float boostTimer = 0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // ��]�͎蓮�Ő���
        SetRandomVelocity();
        changeTimer = directionChangeInterval;
    }

    void Update()
    {
        // ������Ԃ̃^�C�}�[
        if (boostTimer > 0f)
        {
            boostTimer -= Time.deltaTime;
        }
        else if (velocity.magnitude > maxSpeed)
        {
            velocity = Vector2.Lerp(velocity, velocity.normalized * maxSpeed, Time.deltaTime);
        }

        transform.Translate(velocity * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward, velocity.magnitude * 10f * Time.deltaTime); // ���R�ȉ�]

        changeTimer -= Time.deltaTime;
        if (changeTimer <= 0f)
        {
            SetRandomVelocity();
            changeTimer = directionChangeInterval;
        }

        Vector2 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1) velocity.x *= -1;
        if (pos.y < 0 || pos.y > 1) velocity.y *= -1;
    }

    void SetRandomVelocity()
    {
        velocity = Random.insideUnitCircle.normalized * Random.Range(minSpeed, maxSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂ŉ���
        Vector2 impactDir = (transform.position - collision.transform.position).normalized;
        velocity = impactDir * boostSpeed;
        boostTimer = boostDuration;

        // ��]�ǉ��i��]���x�ɉe���j
        float direction = Random.value < 0.5f ? -1f : 1f;
        transform.Rotate(Vector3.forward, rotationBoost * direction);
    }
}
