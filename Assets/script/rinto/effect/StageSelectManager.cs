using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StageSelectManager : MonoBehaviour
{
    public Transform[] stagePoints;
    public float moveSpeed = 5f;
    private int currentIndex = 0;
    private bool isMoving = false;

    public CanvasGroup fadePanel; // ← 黒フェード用
    public float fadeDuration = 1f;
    public float delayBeforeLoad = 0.6f;

    private void Start()
    {
        transform.position = stagePoints[currentIndex].position;
        if (fadePanel != null)
        {
            fadePanel.alpha = 0f;
            fadePanel.blocksRaycasts = false;
        }
    }

    private void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.D)) MoveToIndex(currentIndex + 1);
            else if (Input.GetKeyDown(KeyCode.A)) MoveToIndex(currentIndex - 1);
            else if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(FadeAndLoad());
            else if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                for (int i = 0; i < stagePoints.Length; i++)
                {
                    if (Vector2.Distance(stagePoints[i].position, mousePos) < 0.5f)
                    {
                        MoveToIndex(i);
                        break;
                    }
                }
            }
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, stagePoints[currentIndex].position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, stagePoints[currentIndex].position) < 0.01f)
            {
                transform.position = stagePoints[currentIndex].position;
                isMoving = false;
            }
        }
    }

    void MoveToIndex(int newIndex)
    {
        if (newIndex >= 0 && newIndex < stagePoints.Length)
        {
            currentIndex = newIndex;
            isMoving = true;
        }
    }

    IEnumerator FadeAndLoad()
    {
        if (fadePanel != null)
        {
            fadePanel.blocksRaycasts = true;
            float timer = 0f;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                fadePanel.alpha = Mathf.Clamp01(timer / fadeDuration);
                yield return null;
            }
        }

        yield return new WaitForSeconds(delayBeforeLoad);

        string sceneName = "Stage" + (currentIndex + 1);
        SceneManager.LoadScene(sceneName);
    }
}
