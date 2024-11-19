using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public MonsterStatSO statSO;
    private SpriteRenderer sprite;
    private MonsterHpSystem hpSystem;
    protected GameObject player;
    protected Rigidbody rb;

    // Start is called before the first frame update
    protected void Start()
    {
        player = GameManager.Instance.player.gameObject;
        sprite = GetComponent<SpriteRenderer>();
        hpSystem = GetComponent<MonsterHpSystem>();
        rb = GetComponent<Rigidbody>();
        StatUpdate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flip();
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Move(direction);
    }

    protected abstract void Move(Vector3 direction);

    void Flip()
    {
        sprite.flipX = (transform.position.x - player.transform.position.x > 0);
    }    

    void StatUpdate()
    {
        //TODO :: 시간에 지남에 따라 체력과 공격력이 증가해야됨
    }
}
