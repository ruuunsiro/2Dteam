using UnityEngine;

public class InkSquid : MonoBehaviour
{
    public GameObject inkPanelObject; // UIã‚É‚ ‚é–n—p‚ÌPanel
    public float inkDuration = 2f;     // –n‚Ì•\¦ŠÔ
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
            hasTriggered = false; // Ä‚Ñ‹ß‚Ã‚¢‚½‚Æ‚«‚à”½‰‚·‚é‚È‚ç‚±‚ê‚ğ true ‚É‚µ‚È‚¢
        }
    }
}
