using UnityEngine;

public class BlockController : MonoBehaviour
{
    public Transform player; // �÷��̾� Transform
    public GameObject[] platforms; // ��� ��(���) �迭
    public float platformWidth = 10f; // �� ����� �ʺ�

    private int currentIndex = -1; // ���� ���� �ε���
    private int previousIndex = -1; // ���� ���� �ε���

    public void OnPlayerEnterBlock(Block block) // [����� ��� �� �ƴ϶�, ����� ��ȣ�� ��� �� �� ������?]
    {
        // ���� ����� �ε��� ��������
        int newIndex = GetPlatformIndex(block.gameObject);

        // ���� ����� �ε������� ���� ��� 1�� ����� ���������� �̵� [1���� �ƴ϶� 0��]
        if (previousIndex != -1 && newIndex < previousIndex)
        {
            // [��� �̵� / �迭���� ����� ���� �ݴ������� �ֱ� / ��ü ����� index ��ȭ ������ �ʿ��մϴ�.]
            Debug.Log("���� �̵�");
        }
        // ���� ����� �ε������� ���� ��� 1�� ����� ���������� �̵� [1���� �ƴ϶� 2��]
        else if (previousIndex != -1 && newIndex > previousIndex)
        {
            Debug.Log("������ �̵�");
        }

        // �ε��� ������Ʈ
        previousIndex = currentIndex;
        currentIndex = newIndex;
    }

    // ����� �ε����� �������� �Լ�
    private int GetPlatformIndex(GameObject platform)
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i] == platform)
                return i;
        }
        return -1; // �÷����� �迭�� ������ -1 ��ȯ
    }
}