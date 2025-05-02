using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Spawner : MonoBehaviour
{
   public Transform[] spawnPoint;
   public SpawnData[] spawnData;
   int level;
   float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.FloorToInt (GameManager.instance.gameTime / 10f);
        
        if ( timer > spawnData[level].spawnTime)
        {
            timer = 0f;
            Spawn();
        }
        
    }

    void Spawn()
    {
       GameObject enemy = GameManager.instance.pool.Get(0);
       enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
       enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime; // 소환시간
    public int spriteType;
    public int health; // 체력
    public float speed; // 속도
}