using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockController blockController; // BlockController 참조

    void Start()
    {
        // 씬 내의 BlockController를 찾음
        blockController = FindObjectOfType<BlockController>();
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어가 트리거에 닿았는지 확인
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // BlockController에 현재 블록을 전달
            blockController.OnPlayerEnterBlock(this);
        }
    }
}
