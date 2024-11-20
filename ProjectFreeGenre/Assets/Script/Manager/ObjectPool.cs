using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    // 오브젝트 풀 데이터를 정의할 데이터 모음 정의
    [System.Serializable]
    public class ObectPoolMonster
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<ObectPoolMonster> objectPools;
    public Dictionary<string, Queue<GameObject>> objectPoolsDictionary;

    [SerializeField] private float time;
    [SerializeField] private float timeTmp;

    private void Awake()
    {
        ObjectInit();
        timeTmp = time;
    }

    private void Update()
    {
        if(timeTmp > 0)
        {
            timeTmp -= Time.deltaTime;
        }
        else
        {
            MonsterRespawn();
            timeTmp = time;
        }

    }

    public void MonsterRespawn()
    {
        int monster_Ran = Random.Range(0, 3);

        if (monster_Ran < 1)
            SpawnFromObjectPool("Imp");
        else
            SpawnFromObjectPool("UnderTaker");
    }

    private void ObjectInit()
    {
        objectPoolsDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (ObectPoolMonster pool in objectPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            objectPoolsDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromObjectPool(string tag)
    {
        if (!objectPoolsDictionary.ContainsKey(tag))
            return null;

        GameObject obj = objectPoolsDictionary[tag].Dequeue();
        objectPoolsDictionary[tag].Enqueue(obj);
        obj.SetActive(true);

        GameManager.Instance.monsters.Add(obj);
        return obj;
    }
}
