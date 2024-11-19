using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMove;
    public PlayerEquipment equipment;

    private void Awake()
    {
        GameManager.Instance.player = this;
        playerMove = GetComponent<PlayerMovement>();
    }
}
