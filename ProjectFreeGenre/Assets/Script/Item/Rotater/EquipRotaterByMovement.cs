using System.Collections;
using UnityEngine;

public abstract class EquipRotaterByMovement : EquipRotater
{
    protected Vector2 frontDir;

    protected virtual void Start()
    {
        GameManager.Instance.player.playerMove.OnDirectionChanged += OnDirectionChange;
    }

    public void OnDirectionChange(Vector2 direction)
    {
        frontDir = direction;
        routine = StartCoroutine(ChangeDiredtionAfterDeactivated());
    }

    public override void RemoveAction()
    {
        GameManager.Instance.player.playerMove.OnDirectionChanged -= OnDirectionChange;
        base.RemoveAction();
    }
}