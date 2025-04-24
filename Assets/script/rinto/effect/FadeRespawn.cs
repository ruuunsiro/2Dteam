using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeRespawn : MonoBehaviour
{
    public Image fadeImage; // �~�`��UI�摜���C���X�y�N�^�[���犄�蓖��
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
        else if (collision.CompareTag("comeback")) // ���ꂪ���X�|�[������
        {
            if (!isFading)
                StartCoroutine(FadeAndRespawn());
        }
    }

    private System.Collections.IEnumerator FadeAndRespawn()
    {
        isFading = true;

        // �t�F�[�h�C���i�����Ȃ�j
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = timer / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // ���X�|�[���܂��̓V�[�������[�h
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }
        else
        {
            Debug.LogWarning("���X�|�[���n�_���ݒ肳��Ă��܂���I");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // �t�F�[�h�A�E�g�i���邭�Ȃ�j
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
