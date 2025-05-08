using UnityEngine;

public class InkSquid : MonoBehaviour
{
    public GameObject inkPanelObject; // UI��ɂ���n�p��Panel
    public float inkDuration = 2f;     // �n�̕\������
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
            hasTriggered = false; // �Ăы߂Â����Ƃ�����������Ȃ炱��� true �ɂ��Ȃ�
        }
    }
}
