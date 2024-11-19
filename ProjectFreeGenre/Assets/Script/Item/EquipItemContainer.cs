using UnityEngine;

public class EquipItemContainer : MonoBehaviour
{
    private Vector2 frontDir;

    private void Start()
    {
        // 플레이어측 Event 받아서 작성.
    }

    public void OnDirectionChange(Vector2 direction)
    {
        frontDir = direction;
        Vector3 dir = transform.forward * frontDir.y + transform.right * frontDir.x;
        transform.right = dir;
    }
}