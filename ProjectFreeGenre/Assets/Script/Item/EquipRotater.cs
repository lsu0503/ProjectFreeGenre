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

public class EquipFlip : EquipRotater
{
    protected override void ChangeDirection()
    {
        if(frontDir.x >= 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        else
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
    }
}

public class EquipDirection : EquipRotater
{
    protected override void ChangeDirection()
    {
        
    }
}
