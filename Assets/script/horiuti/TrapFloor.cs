using UnityEngine;

public class TrapFloor : MonoBehaviour
{
    public float moveDistance = 5f;    // —Ž‚¿‚é‹——£
    public float moveSpeed = 10f;       // —Ž‚¿‚é‘¬‚³

    private bool isMoving = false;
    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.down * moveDistance;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (transform.position == targetPos)
            {
                isMoving = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
        }
    }

    // 3D‚ÌŽž‚Í‚±‚Á‚¿‚ðŽg‚Á‚Ä‚Ë
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
        }
    }
    */
}
