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

    private void Awake()
    {
        GameManager.Instance.objectPool = this;
        ObjectInit();
    }

    private void ObjectInit()
    {
        objectPoolsDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (ObectPoolMonster pool in objectPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, this.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            objectPoolsDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromObjectPool(string tag, Vector3 pos)
    {
        if (!objectPoolsDictionary.ContainsKey(tag))
            return null;

        GameObject obj = objectPoolsDictionary[tag].Dequeue();
        objectPoolsDictionary[tag].Enqueue(obj);
        GameManager.Instance.monsterGenerator.StatUpdate(obj);
        obj.transform.position = pos;
        obj.SetActive(true);

        GameManager.Instance.monsters.Add(obj);
        return obj;
    }
}
