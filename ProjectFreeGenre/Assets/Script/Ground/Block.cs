using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int blockID;

    BlockManager blockManager;

    private void Awake()
    {
        blockManager = FindAnyObjectByType<BlockManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            blockManager.curBlockID = blockID;
            blockManager.MoveBlock();
        }
    }
}
