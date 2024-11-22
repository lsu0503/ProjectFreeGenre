 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterGenerator : MonoBehaviour
{
    public GameObject boss;
    [SerializeField] private bool isBoss;

    private int stageLevel;//무슨 스테이지인지
    [SerializeField] public int monsterLevel;//현재난이도 및 등장하는 적의 수량
    [SerializeField] private float distanceThreashold;
    [SerializeField] private int spawnAmountPerSpawn;
    [SerializeField] private float levelUpRate;
    [SerializeField] private int maxLevel;

    [SerializeField] float increasePercent = 0.25f;//증가량

    [SerializeField] private float spawnTime;
    private float spawnTimer;

    private void Awake()
    {
        GameManager.Instance.monsterGenerator = this;
    }

    private void Start()
    {
        stageLevel = SceneManager.GetActiveScene().buildIndex;
        monsterLevel = 0;
        isBoss = false;
        MimicSpawn();
        Invoke("MonsterLevelUp", levelUpRate);
    }

    private void FixedUpdate()
    {
        if(isBoss == false)
        {
            if (spawnTimer > 0)
            {
                spawnTimer -= Time.deltaTime;
            }
            else
            {
                MonsterSpawn(spawnAmountPerSpawn + monsterLevel);
                spawnTimer = spawnTime - monsterLevel;
            }
        }
    }

    public void MonsterLevelUp()
    {
        monsterLevel++;
        if(monsterLevel >= maxLevel)
        {
            isBoss = true;
            BossSpawn();
        }
        else
        {
            MimicSpawn();
            Invoke("MonsterLevelUp", levelUpRate);
        }
    }

    public void StatUpdate(GameObject obj)
    {
        Monster monsterObj = obj.GetComponent<Monster>();
        MonsterStatSO monsterStat = monsterObj.statSO;

        monsterObj.hp = monsterStat.hp * (1.0f + (increasePercent * monsterLevel));
        monsterObj.attackBody = monsterStat.attackBody * (1.0f + (increasePercent * monsterLevel));
        monsterObj.attackBullet = monsterStat.attackBullet * (1.0f + (increasePercent * monsterLevel));
    }

    public void MonsterSpawn(int _spawnCycle)
    {
        int spawnCntTmp = 0;
        Vector3 spawnPos;
        Vector3 playerPosition = GameManager.Instance.player.transform.position;

        while (spawnCntTmp < _spawnCycle)
        {
            spawnPos = SetSpawnPosition();

            int monster_Ran = Random.Range(0, 12);
            if (monster_Ran < 7)
                GameManager.Instance.objectPool.SpawnFromObjectPool("Imp", spawnPos);
            else
                GameManager.Instance.objectPool.SpawnFromObjectPool("UnderTaker", spawnPos);

            spawnCntTmp++;
        }
    }

    public void BossSpawn()
    {
        GameObject bossObj = Instantiate(boss, SetSpawnPosition(), Quaternion.identity);
        StatUpdate(bossObj);
    }

    public void MimicSpawn()
    {
        GameManager.Instance.objectPool.SpawnFromObjectPool("Mimic", SetSpawnPosition());
    }

    public Vector3 SetSpawnPosition()
    {
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        Vector3 spawnPos;
        do
        {
            if (stageLevel == 1)
                spawnPos = SetSpawnPositionStage1();
            else
                spawnPos = SetSpawnPositionStage2();
        } while (Vector3.Distance(spawnPos, playerPosition) < distanceThreashold);

        return spawnPos;
    }

    public Vector3 SetSpawnPositionStage1()
    {
        float randomXPos = Random.Range(-8f, 8f);
        float randomZPos = Random.Range(-8f, 8f);
        Vector3 spawnPos = new Vector3(randomXPos, 0, randomZPos);

        return spawnPos;
    }

    public Vector3 SetSpawnPositionStage2()
    {
        float randomXPos = Random.Range(GameManager.Instance.player.transform.position.x - 8f, GameManager.Instance.player.transform.position.x + 8f);
        float randomZPos = Random.Range(-8f, 8f);
        Vector3 spawnPos = new Vector3(randomXPos, 0, randomZPos);

        return spawnPos;
    }
}
