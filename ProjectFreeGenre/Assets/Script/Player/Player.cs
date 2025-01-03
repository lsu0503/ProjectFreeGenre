using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MonsterStatSO statSO;
    public PlayerMovement playerMove;
    public PlayerEquipment equipment;
    public Interaction interaction;
    public PlayerStat playerStat;
    public PlayerKnockback knockback;

    public Rigidbody rb;

    private void Awake()
    {
        GameManager.Instance.player = this;
        playerMove = GetComponent<PlayerMovement>();
        playerStat = GetComponent<PlayerStat>();
        equipment = GetComponent<PlayerEquipment>();
        interaction = GetComponentInChildren<Interaction>();
        knockback = GetComponent<PlayerKnockback>();
        knockback.SetPlayer(this);

        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            IKnockback knockbackObject = collision.gameObject.GetComponent<IKnockback>();
            if (knockbackObject != null)
            {
                Vector3 direction = collision.gameObject.transform.position - transform.position;
                knockbackObject.ApplyKnockback(direction, statSO.knockBackPower);
            }
        }
    }
}
