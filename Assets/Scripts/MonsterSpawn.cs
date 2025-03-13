using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public ObjectPool objectPool;
    public float spawnCooltime = 1.5f; // ���� ���� ����
    public Vector3 spawnPositionA = new Vector3(40, -3, -1); // ���� ���� ��ġ
    public Vector3 spawnPositionB = new Vector3(40, -3, -2); // ���� ���� ��ġ
    public Vector3 spawnPositionC = new Vector3(40, -3, -3); // ���� ���� ��ġ

    private string[] monsterTypes = { "MonsterA", "MonsterB", "MonsterC" }; // ���� ����

    private void Start()
    {
        InvokeRepeating(nameof(SpawnMonster), 1f, spawnCooltime);
    }

    private void SpawnMonster()
    {
        string randomMonster = monsterTypes[Random.Range(0, monsterTypes.Length)];
        if(randomMonster == "MonsterA")
            objectPool.GetMonster(randomMonster, spawnPositionA);
        else if (randomMonster == "MonsterB")
            objectPool.GetMonster(randomMonster, spawnPositionB);
        else if (randomMonster == "MonsterC")
            objectPool.GetMonster(randomMonster, spawnPositionC);

    }
}
