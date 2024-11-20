using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    // 오브젝트 풀 데이터를 정의할 데이터 모음 정의
    [System.Serializable]
    public class ObectPool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<ObectPool> objectPools;
    public Dictionary<string, Queue<GameObject>> objectPoolsDictionary;

    private void Awake()
    {
        ObjectInit();
    }

    private void ObjectInit()
    {
        objectPoolsDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (ObectPool pool in objectPools)
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
        return obj;
    }
}
