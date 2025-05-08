using UnityEngine;

public class InkSquidFade : MonoBehaviour
{
    public CanvasGroup inkCanvasGroup;  // 墨パネルのCanvasGroup
    public float fadeDuration = 0.5f;   // フェードイン/アウトの速さ
    public float inkVisibleTime = 2f;   // 墨が見えている時間
    private bool isFading = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeInkRoutine());
        }
    }

    private System.Collections.IEnumerator FadeInkRoutine()
    {
        isFading = true;

        // フェードイン
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // 一定時間表示
        yield return new WaitForSeconds(inkVisibleTime);

        // フェードアウト
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));

        isFading = false;
    }

    private System.Collections.IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            inkCanvasGroup.alpha = newAlpha;
            yield return null;
        }

        inkCanvasGroup.alpha = endAlpha;
    }
}
