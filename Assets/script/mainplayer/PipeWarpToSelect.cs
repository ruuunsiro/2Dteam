using UnityEngine;
using UnityEngine.SceneManagement;

public class PipeWarpToSelect : MonoBehaviour
{
    private bool isWarping = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isWarping && other.CompareTag("Pipe"))
        {
            isWarping = true;
            FadeCenter.Instance.FadeOutToCenter(() =>
            {
                SceneManager.LoadScene("StageSelect");
            });
        }
    }
}
