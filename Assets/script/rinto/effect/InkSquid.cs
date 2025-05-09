using UnityEngine;

public class InkSquid : MonoBehaviour
{
    public GameObject inkPanelObject; // UI上にある墨用のPanel
    public float inkDuration = 2f;     // 墨の表示時間
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            ShowInk();
        }
    }

    void ShowInk()
    {
        if (inkPanelObject != null)
        {
            inkPanelObject.SetActive(true);
            Invoke(nameof(HideInk), inkDuration);
        }
    }

    void HideInk()
    {
        if (inkPanelObject != null)
        {
            inkPanelObject.SetActive(false);
            hasTriggered = false; // 再び近づいたときも反応するならこれを true にしない
        }
    }
}
