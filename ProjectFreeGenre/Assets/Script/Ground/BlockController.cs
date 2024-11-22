using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private Transform player; // �÷��̾� Transform
    [SerializeField] private List<GameObject> platforms = new List<GameObject>(); // ��� ��(���) �迭
    [SerializeField] private float platformWidth = 10f; // �� ����� �ʺ�

    public int currentIndex = -1; // ���� ���� �ε���
    private int previousIndex = -1; // ���� ���� �ε���

    private GameObject prevBlock;

    private void Start()
    {
        player = GameManager.Instance.player.gameObject.transform;

        foreach(GameObject block in platforms)
        {
            block.GetComponent<Block>().SetBlockContainer(this);
        }
    }

    public void OnPlayerEnterBlock(Block block)
    {
        // ���� ����� �ε��� ��������
        int newIndex = GetPlatformIndex(block.gameObject);
        int previousIndex = GetPlatformIndex(prevBlock);

        // ���� ����� �ε������� ���� ��� 1�� ����� �������� �̵�
        if (previousIndex != -1 && newIndex < previousIndex)
        {
            MoveBlockToStart(platforms.Count - 1);
        }
        // ���� ����� �ε������� ū ��� 0�� ����� ���������� �̵�
        else if (previousIndex != -1 && newIndex > previousIndex)
        {
            MoveBlockToEnd(0);
        }

        prevBlock = block.gameObject;
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
            Vector3 offset = newPosition - block.transform.position;
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
            Vector3 offset = newPosition - block.transform.position;
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