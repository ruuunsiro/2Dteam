using UnityEngine;

public class Pufferfish : MonoBehaviour
{
    public float detectionRange = 3f;
    public GameObject spikes; // �g�Q�̕\���I�u�W�F�N�g
    public Transform player;
    private bool isSpiked = false;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRange && !isSpiked)
        {
            isSpiked = true;
            spikes.SetActive(true);
        }
        else if (distance >= detectionRange && isSpiked)
        {
            isSpiked = false;
            spikes.SetActive(false);
        }

        Swim();
    }

    void Swim()
    {
        float wave = Mathf.Sin(Time.time * 2f) * 0.5f;
        transform.Translate(new Vector2(0.5f * Time.deltaTime, wave * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSpiked && collision.CompareTag("Player"))
        {
            // �ucomeback�v�g���K�[�ɓ��������Ɠ��l�̏������s���ɂ́A
            // �����Ńv���C���[��"comeback"�^�O�����I�u�W�F�N�g�𓥂܂���悤�ɂ��邩�A
            // �܂��́A�����Œ���Respawn�֐����Ăяo���݌v�ɂ��邱�Ƃ��\�ł��B

            // �� ����́ucomeback�v�]�[���ɐG���Ǝ��ʂƂ����O��Ȃ̂ŁA
            // �t�O�̃g�Q�Ɂucomeback�v�^�O�����Ă�����OK�ł��i���L�⑫�Q�Ɓj
        }
    }
}
