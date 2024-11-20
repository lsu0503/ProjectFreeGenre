using UnityEngine;

public class EquipDirection : EquipRotaterByMovement
{
    protected override void ChangeDirection()
    {
        if(frontDir.magnitude >= ConstantCollection.attackDirectionThreashold)
        {
            float angle = Mathf.Atan2(-frontDir.y, frontDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }
    }
}
