using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterStatSO statSO;
    private SpriteRenderer sprite;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player.gameObject;
        sprite = GetComponent<SpriteRenderer>();
        StatUpdate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flip();
        Move();
    }

    void Move()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > statSO.distance)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * statSO.speed * Time.deltaTime;
        }
    }
    void Flip()
    {
        sprite.flipX = (transform.position.x - player.transform.position.x > 0);
    }    

    void StatUpdate()
    {
        //TODO :: 시간에 지남에 따라 체력과 공격력이 증가해야됨
    }
}
