using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public float attack;//�Ѿ� ������
    [SerializeField] float speed;
    public Vector3 direction;//���󰡴� ����

    private void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
