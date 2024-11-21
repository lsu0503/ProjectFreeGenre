using System;
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
    public MonsterGenerator monsterGenerator;
    public List<GameObject> monsters = new List<GameObject>();

    public SceneController sceneController;
    public event Action OnGameOverEvent;
    public event Action OnGameClearEvent;

    public float currentTIme;

    private void Start()
    {
        currentTIme = 0.0f;
    }

    private void FixedUpdate()
    {
        currentTIme += Time.deltaTime;
    }

    public void GameOver()
    {
        OnGameOverEvent?.Invoke();
    }

    public void GameClear()
    {
        OnGameClearEvent?.Invoke();
    }

    public void ClearManager()
    {
        _player = null;
        objectPool = null;
        monsterGenerator = null;
        monsters.Clear();

        sceneController = null;
        OnGameClearEvent = null;
        OnGameOverEvent = null;

        currentTIme = 0.0f;
    }
}
