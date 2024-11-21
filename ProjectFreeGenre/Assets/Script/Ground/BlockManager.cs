using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Block[] blocks;
    public Transform player;
    public int curBlockID;

    public void MoveBlock()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (curBlockID == 0 || curBlockID == 1)
            {
                blocks[6].transform.Translate(blocks[6].transform.right * -100);
                blocks[7].transform.Translate(blocks[7].transform.right * -100);
            }
            if (curBlockID == 2 || curBlockID == 3)
            {
                blocks[8].transform.Translate(blocks[8].transform.right * -100);
                blocks[9].transform.Translate(blocks[9].transform.right * -100);
            }
            if (curBlockID == 4 || curBlockID == 5)
            {
                blocks[0].transform.Translate(blocks[0].transform.right * -100);
                blocks[1].transform.Translate(blocks[1].transform.right * -100);
            }
            if (curBlockID == 6 || curBlockID == 7)
            {
                blocks[2].transform.Translate(blocks[2].transform.right * -100);
                blocks[3].transform.Translate(blocks[3].transform.right * -100);
            }
            if (curBlockID == 8 || curBlockID == 9)
            {
                blocks[4].transform.Translate(blocks[4].transform.right * -100);
                blocks[5].transform.Translate(blocks[5].transform.right * -100);
            }
        }
        else if( Input.GetKey(KeyCode.D))
        {
            if (curBlockID == 0 || curBlockID == 1)
            {
                blocks[4].transform.Translate(blocks[4].transform.right * 100);
                blocks[5].transform.Translate(blocks[5].transform.right * 100);
            }
            if (curBlockID == 2 || curBlockID == 3)
            {
                blocks[6].transform.Translate(blocks[6].transform.right * 100);
                blocks[7].transform.Translate(blocks[7].transform.right * 100);
            }
            if (curBlockID == 4 || curBlockID == 5)
            {
                blocks[8].transform.Translate(blocks[8].transform.right * 100);
                blocks[9].transform.Translate(blocks[9].transform.right * 100);
            }
            if (curBlockID == 6 || curBlockID == 7)
            {
                blocks[0].transform.Translate(blocks[4].transform.right * 100);
                blocks[1].transform.Translate(blocks[5].transform.right * 100);
            }
            if (curBlockID == 8 || curBlockID == 9)
            {
                blocks[2].transform.Translate(blocks[2].transform.right * 100);
                blocks[3].transform.Translate(blocks[3].transform.right * 100);
            }
        }

    }
}