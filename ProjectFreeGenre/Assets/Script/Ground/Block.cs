using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockController blockController; // BlockController ����

    public void SetBlockContainer(BlockController container)
    {
        // �� ���� BlockController�� ã��
        blockController = container;
    }

    void OnTriggerEnter(Collider other)
    {
        // �÷��̾ Ʈ���ſ� ��Ҵ��� Ȯ��
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // BlockController�� ���� ����� ����
            blockController.OnPlayerEnterBlock(this);
        }

        // ���ͳ� �������� Ʈ���ſ� ��Ҵ��� Ȯ��
        else if(other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("1");
            // �ش� ���� �� �������� �ڽ� Ŭ������ ����
            other.gameObject.transform.SetParent(this.transform);
        }
    }
}
