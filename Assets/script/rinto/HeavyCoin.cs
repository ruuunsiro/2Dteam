using UnityEngine;

public class HeavyCoin : MonoBehaviour
{
    public float jumpReduction = 2f; // ジャンプ力を減らす値

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.DecreaseJumpForce(jumpReduction);
                Destroy(gameObject); // コインを消す
            }
        }
    }
}
