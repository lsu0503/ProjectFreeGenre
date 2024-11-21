using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockController blockController; // BlockController 참조

    public void SetBlockContainer(BlockController container)
    {
        // 씬 내의 BlockController를 찾음
        blockController = container;
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어가 트리거에 닿았는지 확인
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // BlockController에 현재 블록을 전달
            blockController.OnPlayerEnterBlock(this);
        }

        // 몬스터나 아이템이 트리거에 닿았는지 확인
        else if(other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("1");
            // 해당 몬스터 및 아이템을 자식 클래스로 변경
            other.gameObject.transform.SetParent(this.transform);
        }
    }
}
