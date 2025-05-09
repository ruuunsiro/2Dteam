using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerRespawn : MonoBehaviour
{
    public Transform currentRespawnPoint; // 現在のリスポーン地点
    public TMP_Text respawnCountText; // リスポーン回数表示用（TMP）
    public GameObject respawnUI; // 黒背景（親オブジェクト）

    public GameObject canvasObject;

    private Rigidbody2D rb;
    private static int respawnCount = 0; // リスポーン回数

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnUI.SetActive(false); // 開始時は非表示
        canvasObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RespawnPoint"))
        {
            currentRespawnPoint = collision.transform;
        }

        if (collision.CompareTag("comeback"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        if (currentRespawnPoint != null)
        {
            respawnCount++;

            // プレイヤーを非アクティブ化（1秒間に複数カウントされるのを防ぐ）
            gameObject.SetActive(false);

            // UI表示の流れ
            ShowRespawnUI();

            // 1秒後にシーンをリロード
            Invoke("ReloadScene", 1f);
        }
        else
        {
            Debug.LogWarning("リスポーン地点が設定されていません！");
        }
    }


    void ShowRespawnUI()
    {
        // 黒背景とテキスト表示
        respawnUI.SetActive(true);
        respawnCountText.gameObject.SetActive(true);
        respawnCountText.text = "-" + respawnCount.ToString();

        // 1秒後に非表示
        Invoke("HideRespawnUI", 1f);
    }

    void HideRespawnUI()
    {
        respawnUI.SetActive(false);
        respawnCountText.gameObject.SetActive(false);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
