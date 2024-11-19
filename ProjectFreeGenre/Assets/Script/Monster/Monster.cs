using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public MonsterStatSO statSO;
    private SpriteRenderer sprite;

    public MonsterHpSystem hpSystem;
    public float hpTmp;

    protected GameObject player;
    public Rigidbody rb;

    // Start is called before the first frame update
    protected void Start()
    {
        player = GameManager.Instance.player.gameObject;
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
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
        //
        hpTmp = statSO.hp;
        hpSystem.HpUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        IKnockback knockbackObject = collision.gameObject.GetComponent<IKnockback>();
        if (knockbackObject != null)
        {
            Vector3 direction = collision.gameObject.transform.position - transform.position;
            knockbackObject.ApplyKnockback(direction, statSO.knockBackPower);
        }
    }
}
