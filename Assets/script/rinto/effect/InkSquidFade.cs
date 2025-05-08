using UnityEngine;

public class InkSquidFade : MonoBehaviour
{
    public CanvasGroup inkCanvasGroup;  // �n�p�l����CanvasGroup
    public float fadeDuration = 0.5f;   // �t�F�[�h�C��/�A�E�g�̑���
    public float inkVisibleTime = 2f;   // �n�������Ă��鎞��
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

        // �t�F�[�h�C��
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // ��莞�ԕ\��
        yield return new WaitForSeconds(inkVisibleTime);

        // �t�F�[�h�A�E�g
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
