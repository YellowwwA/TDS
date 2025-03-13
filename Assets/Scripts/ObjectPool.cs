using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int num;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDic;

    private Queue<GameObject> monsterPool = new Queue<GameObject>();

    private void Start()
    {
        poolDic = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.num; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDic.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetMonster(string tag, Vector2 position)
    {
        if (!poolDic.ContainsKey(tag)) return null;

        GameObject objectToSpawn = poolDic[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;

        poolDic[tag].Enqueue(objectToSpawn); // 재사용 가능하게 다시 큐에 추가
        return objectToSpawn;
    }

    // 몬스터 반환
    public void ReturnMonster(GameObject monster)
    {
        monster.SetActive(false); // 비활성화
        monsterPool.Enqueue(monster); // 다시 큐에 넣기
    }
}