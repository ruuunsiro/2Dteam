using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeRespawn : MonoBehaviour
{
    public Image fadeImage; // 円形のUI画像をインスペクターから割り当て
    public float fadeDuration = 0.5f;
    public string respawnTag = "RespawnPoint";

    private Transform respawnPoint;
    private bool isFading = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(respawnTag))
        {
            respawnPoint = collision.transform;
        }
        else if (collision.CompareTag("comeback")) // これがリスポーン条件
        {
            if (!isFading)
                StartCoroutine(FadeAndRespawn());
        }
    }

    private System.Collections.IEnumerator FadeAndRespawn()
    {
        isFading = true;

        // フェードイン（黒くなる）
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = timer / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // リスポーンまたはシーンリロード
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }
        else
        {
            Debug.LogWarning("リスポーン地点が設定されていません！");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // フェードアウト（明るくなる）
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = 1 - (timer / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        isFading = false;
    }
}
