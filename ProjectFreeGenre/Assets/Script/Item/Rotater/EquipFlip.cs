using UnityEngine;

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
