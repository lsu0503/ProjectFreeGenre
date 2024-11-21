 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterGenerator : MonoBehaviour
{
    private int stageLevel;
    public int levelScaling = 0;
    [SerializeField] private float distance;
    [SerializeField] private int spawnCnt;

    [SerializeField] float increasePercent = 0.1f;//Αυ°‘·®

    [SerializeField] private float time;
    private float timeTmp;


    private void Awake()
    {
        GameManager.Instance.monsterGenerator = this;
        LevelScalingUp();
        timeTmp = time;
    }

    private void Start()
    {
        stageLevel = SceneManager.GetActiveScene().buildIndex;
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
        if (stageLevel == 1)
            MonsterSpawnStage1(_spawnCycle);
        else
            MonsterSpawnStage2(_spawnCycle);
    }

    private void MonsterSpawnStage1(int _spawnCycle)
    {
        spawnCnt = 0;

        while(spawnCnt < _spawnCycle)
        {
            float randomXPos = Random.Range(-4f, 12f);
            float randomZPos = Random.Range(-10f, 6f);
            Vector3 spawnPos = new Vector3(randomXPos, 0, randomZPos);

            float distanceTmp = Vector3.Distance(spawnPos, GameManager.Instance.player.transform.position);
            if (distanceTmp < distance)
            {
                continue;
            }

            int monster_Ran = Random.Range(0, 12);
            if (monster_Ran < 5)
                GameManager.Instance.objectPool.SpawnFromObjectPool("Imp", spawnPos);
            else if (monster_Ran < 11)
                GameManager.Instance.objectPool.SpawnFromObjectPool("UnderTaker", spawnPos);
            else
                GameManager.Instance.objectPool.SpawnFromObjectPool("Mimic", spawnPos);

            spawnCnt++;
        }

    }
    private void MonsterSpawnStage2(int _spawnCycle)
    {
        spawnCnt = 0;

        while (spawnCnt < _spawnCycle)
        {
            float randomXPos = Random.Range(GameManager.Instance.player.transform.position.x - 8f, GameManager.Instance.player.transform.position.x + 8f);
            float randomZPos = Random.Range(-10f, 7f);
            Vector3 spawnPos = new Vector3(randomXPos, 0, randomZPos);

            float distanceTmp = Vector3.Distance(spawnPos, GameManager.Instance.player.transform.position);
            if (distanceTmp < distance)
            {
                continue;
            }

            int monster_Ran = Random.Range(0, 12);
            if (monster_Ran < 5)
                GameManager.Instance.objectPool.SpawnFromObjectPool("Imp", spawnPos);
            else if (monster_Ran < 11)
                GameManager.Instance.objectPool.SpawnFromObjectPool("UnderTaker", spawnPos);
            else
                GameManager.Instance.objectPool.SpawnFromObjectPool("Mimic", spawnPos);

            spawnCnt++;
        }
    }
}
