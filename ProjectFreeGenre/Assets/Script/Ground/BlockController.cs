using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public List<GameObject> platforms = new List<GameObject>(); // 모든 땅(블록) 배열
    public float platformWidth = 10f; // 각 블록의 너비

    public int currentIndex = -1; // 현재 땅의 인덱스
    private int previousIndex = -1; // 이전 땅의 인덱스

    public void OnPlayerEnterBlock(Block block)
    {
        // 현재 블록의 인덱스 가져오기
        int newIndex = GetPlatformIndex(block.gameObject);

  
        // 이전 블록의 인덱스보다 작은 경우 1번 블록을 오른쪽으로 이동 [이동 + 배열에서 블록을 빼서 반대편으로 넣기 + 전체 블록의 index 변화 적용이 필요]
        if (previousIndex != -1 && newIndex < previousIndex)
        {
            Debug.Log("왼쪽 이동");
            MoveBlockToStart(platforms.Count - 1);
        }
        // 이전 블록의 인덱스보다 작은 경우 1번 블록을 오른쪽으로 이동 [이동 + 배열에서 블록을 빼서 반대편으로 넣기 + 전체 블록의 index 변화 적용이 필요]
        else if (previousIndex != -1 && newIndex > previousIndex)
        {
            Debug.Log("오른쪽 이동");
            MoveBlockToEnd(0);
        }

        // 인덱스 업데이트
        previousIndex = currentIndex;
        currentIndex = newIndex;
    }

    // 블록을 리스트의 맨 뒤로 이동하는 함수
    private void MoveBlockToEnd(int index)
    {
        if (index >= 0 && index < platforms.Count)
        {
            GameObject block = platforms[index]; // 해당 블록 저장
            platforms.RemoveAt(index); // 리스트에서 제거
            platforms.Add(block); // 리스트의 맨 뒤로 추가

            // 블록의 위치 업데이트
            Vector3 newPosition = platforms[platforms.Count - 2].transform.position; // 마지막 블록 위치 참조
            newPosition.x += platformWidth; // 새로운 위치 계산
            block.transform.position = newPosition;
        }
    }

    // 블록을 리스트의 맨 앞으로 이동하는 함수
    private void MoveBlockToStart(int index)
    {
        if (index >= 0 && index < platforms.Count)
        {
            GameObject block = platforms[index]; // 해당 블록 저장
            platforms.RemoveAt(index); // 리스트에서 제거
            platforms.Insert(0, block); // 리스트의 맨 앞으로 추가

            // 블록의 위치 업데이트
            Vector3 newPosition = platforms[1].transform.position; // 기존 첫 번째 블록 위치 참조
            newPosition.x -= platformWidth; // 새로운 위치 계산
            block.transform.position = newPosition;
        }
    }

    // 블록의 인덱스를 가져오는 함수
    private int GetPlatformIndex(GameObject platform)
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            if (platforms[i] == platform)
                return i;
        }
        return -1; // 플랫폼이 배열에 없으면 -1 반환
    }
}