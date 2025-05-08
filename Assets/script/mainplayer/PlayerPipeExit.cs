using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerPipeExit : MonoBehaviour
{
    public float exitDuration = 1.5f;
    public float exitSpeed = 2f;
    public string nextSceneName = "StageSelect"; // 遷移先のシーン名
    public Image fadePanel; // 画面全体のフェード用パネル

    private bool isExiting = false;
    private float exitTimer = 0f;

    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        // 初期設定でパネルの透明度を0にしておく
        Color color = fadePanel.color;
        color.a = 0f;
        fadePanel.color = color;
    }

    void Update()
    {
        if (isExiting)
        {
            exitTimer += Time.deltaTime;
            float t = Mathf.Clamp01(exitTimer / exitDuration);

            // 右へ移動
            transform.position += Vector3.right * exitSpeed * Time.deltaTime;

            // 徐々に透明に
            float alpha = Mathf.Lerp(1f, 0f, t);
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);

            // フェードアウトのためのパネル
            Color panelColor = fadePanel.color;
            panelColor.a = Mathf.Lerp(0f, 1f, t);  // アルファ値を0から1に変化させる
            fadePanel.color = panelColor;

            // 時間が来たらシーン遷移
            if (exitTimer >= exitDuration)
            {
                StartCoroutine(FadeToScene());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pipe") && !isExiting)
        {
            isExiting = true;

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.gravityScale = 0f;
                rb.isKinematic = true;
            }

            if (playerCollider != null)
                playerCollider.enabled = false;

            // スクリプトを無効にする処理
            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                if (script != this)
                    script.enabled = false;
            }
        }
    }

    private IEnumerator FadeToScene()
    {
        // フェード後にシーン遷移
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(nextSceneName);
    }
}
