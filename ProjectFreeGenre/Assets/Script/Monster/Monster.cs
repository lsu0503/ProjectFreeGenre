using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public MonsterStatSO statSO;
    private SpriteRenderer sprite;
    protected Animator animator;

    public MonsterHpSystem hpSystem;

    protected GameObject player;
    public Rigidbody rb;

    // Start is called before the first frame update
    protected void Start()
    {
        player = GameManager.Instance.player.gameObject;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
        
        hpSystem.HpUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            IDamage damageable = collision.gameObject.GetComponent<IDamage>();
            if (damageable != null)
            {
                damageable.Attacked(statSO.attackBody);
            }

            IKnockback knockbackObject = collision.gameObject.GetComponent<IKnockback>();

            if (knockbackObject != null)
            {
                Vector3 direction = collision.gameObject.transform.position - transform.position;
                knockbackObject.ApplyKnockback(direction, statSO.knockBackPower);
            }
        }
    }
}
