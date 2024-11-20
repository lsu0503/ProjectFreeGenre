using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public float attack;//�Ѿ� ������
    [SerializeField] float speed;
    public Vector3 direction;//���󰡴� ����

    private void OnEnable()
    {
        Invoke("DestroyBullet", 5f);
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            IDamage damageable = collision.gameObject.GetComponent<IDamage>();
            if (damageable != null)
            {
                damageable.Attacked(attack);
            }
            DestroyBullet();
        }
    }
}
