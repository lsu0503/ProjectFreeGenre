using System.Collections.Generic;
using UnityEngine;

public class ItemAttack : MonoBehaviour
{
    private ItemData data;
    private List<GameObject> hitList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if((data.targetLayers & 1 << other.gameObject.layer) != 0)
        {
            if (!hitList.Exists(id => ReferenceEquals(id, other.gameObject)))
            {
                hitList.Add(other.gameObject);

                IDamage targetDamageable = other.gameObject.GetComponent<IDamage>();
                if (targetDamageable != null)
                {
                    targetDamageable.Attacked(data.damage);
                }

                IKnockback targetKnockbackable = other.gameObject.GetComponent<IKnockback>();
                if(targetKnockbackable != null)
                {
                    targetKnockbackable.ApplyKnockback(transform.up, data.knockbackPower);
                }
            }
        }
    }

    public void SetData(ItemData data)
    {
        this.data = data;
    }

    public void ClearHitData()
    {
        hitList.Clear();
    }
}