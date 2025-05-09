using UnityEngine;

public class EnemyMerge : MonoBehaviour
{
    public GameObject partner;                  // ���̂��鑊��i�C���X�y�N�^�[�Ŏw��j
    public float mergeDelay = 10f;              // ���b��ɍ��̂��邩
    public float moveSpeed = 2f;                // ���̂Ɍ������ړ����x
    public GameObject mergedObjectPrefab;       // ���̌�ɐ��������I�u�W�F�N�g

    private float timer = 0f;
    private bool isMerging = false;

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // ���̏����J�n����
        if (!isMerging && timer >= mergeDelay && partner != null)
        {
            isMerging = true;

            // ���V���~�i�I�v�V�����j
            var myFloat = GetComponent<FloatMovement>();
            if (myFloat != null) myFloat.enabled = false;

            var partnerFloat = partner.GetComponent<FloatMovement>();
            if (partnerFloat != null) partnerFloat.enabled = false;

            // ���̃X�N���v�g�i��: SpaceEnemyShooter�j���~�߂�
            var myShooter = GetComponent<SpaceEnemyShooter>();
            if (myShooter != null) myShooter.enabled = false;

            var partnerShooter = partner.GetComponent<SpaceEnemyShooter>();
            if (partnerShooter != null) partnerShooter.enabled = false;
        }

        // ���̒��̈ړ�����
        if (isMerging && partner != null)
        {
            // �݂��ɒ��S�Ɍ������Ĉړ��i�Е����ł�OK�j
            transform.position = Vector3.MoveTowards(transform.position, partner.transform.position, moveSpeed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, partner.transform.position);
            if (distance < 0.1f)
            {
                // ���̊���
                Vector3 center = (transform.position + partner.transform.position) / 2f;
                Instantiate(mergedObjectPrefab, center, Quaternion.identity);
                Destroy(partner);
                Destroy(gameObject);
            }
        }
        if (isMerging)
        {
            var myChase = GetComponent<ChasePlayer>();
            if (myChase != null) myChase.enabled = false;

            if (partner != null)
            {
                var partnerChase = partner.GetComponent<ChasePlayer>();
                if (partnerChase != null) partnerChase.enabled = false;
            }
        }

    }

}
