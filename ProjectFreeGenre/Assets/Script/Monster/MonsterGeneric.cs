 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGeneric : MonoBehaviour
{
    public int levelScaling = 0;

    [SerializeField] float increasePercent = 0.1f;//증가량

    [SerializeField] private float time;
    [SerializeField] private float timeTmp;


    private void Awake()
    {
        GameManager.Instance.monsterGeneric = this;
        LevelScalingUp();
        timeTmp = time;
    }

    private void Update()
    {
        if (timeTmp > 0)
        {
            timeTmp -= Time.deltaTime;
        }
        else
        {
            MonsterRespawn(levelScaling);
            timeTmp = time;
        }
    }

    public void LevelScalingUp()
    {
        levelScaling++;
        Invoke("LevelScalingUp", 60f);
    }

    public void StatUpdate(GameObject obj)
    {
        
        Monster monsterObj = obj.GetComponent<Monster>();
        float baseHP = monsterObj.statSO.hp;
        float basAttackBody = monsterObj.statSO.attackBody;
        float basAttackBullet = monsterObj.statSO.attackBullet;
        int level = GameManager.Instance.monsterGeneric.levelScaling; // 현재 레벨

        monsterObj.hp = baseHP + (baseHP * increasePercent * (level - 1));
        monsterObj.attackBody = basAttackBody + (basAttackBody * increasePercent * (level - 1));
        monsterObj.attackBullet = basAttackBullet + (basAttackBullet * increasePercent * (level - 1));

        Debug.Log(monsterObj.hp);
        Debug.Log(monsterObj.attackBody);
    }

    public void MonsterRespawn(int _spawnCycle)
    {
        for(int i=0; i<_spawnCycle; i++)
        {
            int monster_Ran = Random.Range(0, 3);

            if (monster_Ran < 1)
                GameManager.Instance.objectPool.SpawnFromObjectPool("Imp");
            else
                GameManager.Instance.objectPool.SpawnFromObjectPool("UnderTaker");
        }
    }
}
