using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMove;
    public PlayerEquipment equipment;
    public Interaction interaction;
    public PlayerStat playerStat;

    private void Awake()
    {
        GameManager.Instance.player = this;
        playerMove = GetComponent<PlayerMovement>();
        playerStat = GetComponent<PlayerStat>();
        equipment = GetComponent<PlayerEquipment>();
        interaction = GetComponentInChildren<Interaction>();
    }
}
