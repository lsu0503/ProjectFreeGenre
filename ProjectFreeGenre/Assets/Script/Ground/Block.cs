using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockController blockController; // BlockController ����

    void Start()
    {
        // �� ���� BlockController�� ã��
        blockController = FindObjectOfType<BlockController>();
    }

    void OnTriggerEnter(Collider other)
    {
        // �÷��̾ Ʈ���ſ� ��Ҵ��� Ȯ��
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // BlockController�� ���� ����� ����
            blockController.OnPlayerEnterBlock(this);
        }
    }
}
