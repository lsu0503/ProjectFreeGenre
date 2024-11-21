 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterGenerator : MonoBehaviour
{
    public GameObject boss;
    [SerializeField] private bool isBoss;

    private int stageLevel;//무슨 스테이지인지
    [SerializeField] public int levelScaling;//현재난이도 및 등장하는 적의 수량
    [SerializeField] private float distance;

    [SerializeField] float increasePercent = 0.1f;//증가량

    [SerializeField] private float spawnTime;
    private float spawnTimeTmp;
    Vector3 spawnPos;

    private void Awake()
    {
        GameManager.Instance.monsterGenerator = this;
    }

    private void Start()
    {
        stageLevel = SceneManager.GetActiveScene().buildIndex;
        //levelScaling = 0;
        LevelScalingUp();
    }

    private void Update()
    {
        if(isBoss == false)
        {
            if (spawnTimeTmp > 0)
            {
                spawnTimeTmp -= Time.deltaTime;
            }
            else
            {
                MonsterSpawn(levelScaling);
                spawnTimeTmp = spawnTime;
            }
        }
    }

    public void LevelScalingUp()
    {
        levelScaling++;
        if(levelScaling >= 10)
        {
            isBoss = true;
            if (stageLevel == 1)
                MonsterSpawnStage1(1);
            else
                MonsterSpawnStage2(1);
        }
        else
        {
            isBoss = false;
            Invoke("LevelScalingUp", 60f);
        }
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
        int spawnCntTmp = 0;

        while (spawnCntTmp < _spawnCycle)
        {
            float randomXPos = Random.Range(-4f, 12f);
            float randomZPos = Random.Range(-10f, 6f);
            spawnPos = new Vector3(randomXPos, 0, randomZPos);

            float distanceTmp = Vector3.Distance(spawnPos, GameManager.Instance.player.transform.position);
            if (distanceTmp < distance)
            {
                continue;
            }

            if(isBoss == false)
            {
                int monster_Ran = Random.Range(0, 12);
                if (monster_Ran < 7)
                    GameManager.Instance.objectPool.SpawnFromObjectPool("Imp", spawnPos);
                else
                    GameManager.Instance.objectPool.SpawnFromObjectPool("UnderTaker", spawnPos);
            }
            else
            {
                Instantiate(boss, spawnPos, Quaternion.identity);
                return;
            }

            spawnCntTmp++;
        }
        if (levelScaling % 10 == 0)
            GameManager.Instance.objectPool.SpawnFromObjectPool("Mimic", spawnPos);

    }
    private void MonsterSpawnStage2(int _spawnCycle)
    {
        int spawnCntTmp = 0;

        while (spawnCntTmp < _spawnCycle)
        {
            float randomXPos = Random.Range(GameManager.Instance.player.transform.position.x - 8f, GameManager.Instance.player.transform.position.x + 8f);
            float randomZPos = Random.Range(-10f, 7f);
            spawnPos = new Vector3(randomXPos, 0, randomZPos);

            float distanceTmp = Vector3.Distance(spawnPos, GameManager.Instance.player.transform.position);
            if (distanceTmp < distance)
            {
                continue;
            }

            if (isBoss == false)
            {
                int monster_Ran = Random.Range(0, 12);
                if (monster_Ran < 7)
                    GameManager.Instance.objectPool.SpawnFromObjectPool("Imp", spawnPos);
                else
                    GameManager.Instance.objectPool.SpawnFromObjectPool("UnderTaker", spawnPos);
            }
            else
            {
                Instantiate(boss, spawnPos, Quaternion.identity);
                return;
            }

            spawnCntTmp++;
        }
        if (levelScaling % 10 == 0)
            GameManager.Instance.objectPool.SpawnFromObjectPool("Mimic", spawnPos);
    }
}
