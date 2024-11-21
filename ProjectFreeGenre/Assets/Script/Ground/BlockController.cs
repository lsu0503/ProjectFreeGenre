using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public Transform player; // �÷��̾� Transform
    public List<GameObject> platforms = new List<GameObject>(); // ��� ��(���) �迭
    public float platformWidth = 10f; // �� ����� �ʺ�

    public int currentIndex = -1; // ���� ���� �ε���
    private int previousIndex = -1; // ���� ���� �ε���

    public void OnPlayerEnterBlock(Block block)
    {
        // ���� ����� �ε��� ��������
        int newIndex = GetPlatformIndex(block.gameObject);

  
        // ���� ����� �ε������� ���� ��� 1�� ����� ���������� �̵� [�̵� + �迭���� ����� ���� �ݴ������� �ֱ� + ��ü ����� index ��ȭ ������ �ʿ�]
        if (previousIndex != -1 && newIndex < previousIndex)
        {
            Debug.Log("���� �̵�");
            MoveBlockToStart(platforms.Count - 1);
        }
        // ���� ����� �ε������� ���� ��� 1�� ����� ���������� �̵� [�̵� + �迭���� ����� ���� �ݴ������� �ֱ� + ��ü ����� index ��ȭ ������ �ʿ�]
        else if (previousIndex != -1 && newIndex > previousIndex)
        {
            Debug.Log("������ �̵�");
            MoveBlockToEnd(0);
        }

        // �ε��� ������Ʈ
        previousIndex = currentIndex;
        currentIndex = newIndex;
    }

    // ����� ����Ʈ�� �� �ڷ� �̵��ϴ� �Լ�
    private void MoveBlockToEnd(int index)
    {
        if (index >= 0 && index < platforms.Count)
        {
            GameObject block = platforms[index]; // �ش� ��� ����
            platforms.RemoveAt(index); // ����Ʈ���� ����
            platforms.Add(block); // ����Ʈ�� �� �ڷ� �߰�

            // ����� ��ġ ������Ʈ
            Vector3 newPosition = platforms[platforms.Count - 2].transform.position; // ������ ��� ��ġ ����
            newPosition.x += platformWidth; // ���ο� ��ġ ���
            block.transform.position = newPosition;
        }
    }

    // ����� ����Ʈ�� �� ������ �̵��ϴ� �Լ�
    private void MoveBlockToStart(int index)
    {
        if (index >= 0 && index < platforms.Count)
        {
            GameObject block = platforms[index]; // �ش� ��� ����
            platforms.RemoveAt(index); // ����Ʈ���� ����
            platforms.Insert(0, block); // ����Ʈ�� �� ������ �߰�

            // ����� ��ġ ������Ʈ
            Vector3 newPosition = platforms[1].transform.position; // ���� ù ��° ��� ��ġ ����
            newPosition.x -= platformWidth; // ���ο� ��ġ ���
            block.transform.position = newPosition;
        }
    }

    // ����� �ε����� �������� �Լ�
    private int GetPlatformIndex(GameObject platform)
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            if (platforms[i] == platform)
                return i;
        }
        return -1; // �÷����� �迭�� ������ -1 ��ȯ
    }
}