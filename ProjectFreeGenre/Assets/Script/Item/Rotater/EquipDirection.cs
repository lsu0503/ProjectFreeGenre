using UnityEngine;

public class EquipDirection : EquipRotater
{
    protected override void ChangeDirection()
    {
        if(frontDir.magnitude >= ConstantCollection.attackDirectionThreashold)
        {
            Vector3 dir = transform.forward * frontDir.y + transform.right * frontDir.x;
            transform.rotation = Quaternion.Euler(dir);
        }
    }
}
