using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public MonsterStatSO statSO;
    private SpriteRenderer sprite;
    protected GameObject player;

    // Start is called before the first frame update
    protected void Start()
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

    protected abstract void Move();

    void Flip()
    {
        sprite.flipX = (transform.position.x - player.transform.position.x > 0);
    }    

    void StatUpdate()
    {
        //TODO :: �ð��� ������ ���� ü�°� ���ݷ��� �����ؾߵ�
    }
}
