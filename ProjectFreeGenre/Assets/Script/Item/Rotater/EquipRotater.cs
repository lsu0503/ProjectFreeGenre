using UnityEngine;

public abstract class EquipRotater : MonoBehaviour
{
    protected Vector2 frontDir;

    protected virtual void Start()
    {
        GameManager.Instance.player.playerMove.OnDirectionChanged += OnDirectionChange;
    }

    public void OnDirectionChange(Vector2 direction)
    {
        frontDir = direction;
        ChangeDirection();
    }

    protected abstract void ChangeDirection();
}