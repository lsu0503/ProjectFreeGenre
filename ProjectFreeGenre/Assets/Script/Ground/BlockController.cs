using UnityEngine;

public class BlockController : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public GameObject[] platforms; // 모든 땅(블록) 배열
    public float platformWidth = 10f; // 각 블록의 너비

    private int currentIndex = -1; // 현재 땅의 인덱스
    private int previousIndex = -1; // 이전 땅의 인덱스

    public void OnPlayerEnterBlock(Block block) // [블록을 쏘는 게 아니라, 블록의 번호를 쏘는 게 더 좋겠죠?]
    {
        // 현재 블록의 인덱스 가져오기
        int newIndex = GetPlatformIndex(block.gameObject);

        // 이전 블록의 인덱스보다 작은 경우 1번 블록을 오른쪽으로 이동 [1번이 아니라 0번]
        if (previousIndex != -1 && newIndex < previousIndex)
        {
            // [블록 이동 / 배열에서 블록을 빼서 반대편으로 넣기 / 전체 블록의 index 변화 적용이 필요합니다.]
            Debug.Log("왼쪽 이동");
        }
        // 이전 블록의 인덱스보다 작은 경우 1번 블록을 오른쪽으로 이동 [1번이 아니라 2번]
        else if (previousIndex != -1 && newIndex > previousIndex)
        {
            Debug.Log("오른쪽 이동");
        }

        // 인덱스 업데이트
        previousIndex = currentIndex;
        currentIndex = newIndex;
    }

    // 블록의 인덱스를 가져오는 함수
    private int GetPlatformIndex(GameObject platform)
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i] == platform)
                return i;
        }
        return -1; // 플랫폼이 배열에 없으면 -1 반환
    }
}