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
    public float hp;
    public float attackBody;
    public float attackBullet;

    // Start is called before the first frame update
    protected void Start()
    {
        player = GameManager.Instance.player.gameObject;
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        hpSystem.HpUpdate();

    }

    /*private void OnEnable()
    {
        StatUpdate();
    }*/

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
