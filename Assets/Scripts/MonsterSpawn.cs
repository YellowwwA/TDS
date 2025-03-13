using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public ObjectPool objectPool;
    public float spawnCooltime = 1.5f; // 몬스터 생성 간격
    public Vector3 spawnPositionA = new Vector3(40, -3, -1); // 몬스터 생성 위치
    public Vector3 spawnPositionB = new Vector3(40, -3, -2); // 몬스터 생성 위치
    public Vector3 spawnPositionC = new Vector3(40, -3, -3); // 몬스터 생성 위치

    private string[] monsterTypes = { "MonsterA", "MonsterB", "MonsterC" }; // 몬스터 종류

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
