using UnityEngine;

public class SpaceEnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 2f;
    public float bulletSpeed = 5f;

    private Transform player;
    private float fireTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        // プレイヤーに向かって弾を撃つ
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            FireBullet();
            fireTimer = 0f;
        }
    }

    void FireBullet()
    {
        // プレイヤーの方向を計算
        Vector2 direction = (player.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;  // プレイヤーに向かって弾を発射
        }
    }
}
