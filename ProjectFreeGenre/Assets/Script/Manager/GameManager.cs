using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : Singleton<GameManager>
{
    public Player _player;
    public Player player
    {
        get { return _player; }
        set { _player = value; }
    }

    public ObjectPool objectPool;
    public MonsterGeneration monsterGeneration;
    public List<GameObject> monsters = new List<GameObject>();

}
