using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeCenter : MonoBehaviour
{
    public static FadeCenter Instance;
    public Image fadeImage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void FadeOutToCenter(System.Action onFadeComplete)
    {
        StartCoroutine(FadeCoroutine(onFadeComplete));
    }

    private IEnumerator FadeCoroutine(System.Action onFadeComplete)
    {
        float duration = 1.2f;
        float time = 0f;

        fadeImage.gameObject.SetActive(true);
        fadeImage.material = new Material(Shader.Find("UI/Default")); // í èÌÉ}ÉeÉäÉAÉã

        while (time < duration)
        {
            float t = time / duration;
            fadeImage.color = new Color(0, 0, 0, Mathf.SmoothStep(0, 1, t));
            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = Color.black;
        yield return new WaitForSeconds(0.2f);
        onFadeComplete?.Invoke();
    }
}
