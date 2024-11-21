 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGeneration : MonoBehaviour
{
    public int levelScaling = 0;

    [SerializeField] float increasePercent = 0.1f;//Αυ°‘·®

    [SerializeField] private float time;
    private float timeTmp;


    private void Awake()
    {
        GameManager.Instance.monsterGeneration = this;
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
            MonsterSpawn(levelScaling);
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
        MonsterStatSO monsterStat = monsterObj.statSO;

        monsterObj.hp = monsterStat.hp + (monsterStat.hp * increasePercent * (levelScaling - 1));
        monsterObj.attackBody = monsterStat.attackBody + (monsterStat.attackBody * increasePercent * (levelScaling - 1));
        monsterObj.attackBullet = monsterStat.attackBullet + (monsterStat.attackBullet * increasePercent * (levelScaling - 1));
    }

    public void MonsterSpawn(int _spawnCycle)
    {
        for(int i=0; i<_spawnCycle; i++)
        {
            int monster_Ran = Random.Range(0, 12);

            if (monster_Ran < 5)
                GameManager.Instance.objectPool.SpawnFromObjectPool("Imp");
            else if(monster_Ran < 11)
                GameManager.Instance.objectPool.SpawnFromObjectPool("UnderTaker");
            else
                GameManager.Instance.objectPool.SpawnFromObjectPool("Mimic");
        }
    }
}
