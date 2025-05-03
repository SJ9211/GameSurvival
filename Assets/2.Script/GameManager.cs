using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")] // GameControl 속성
    public float gameTime; // 흐르는 시간
    public float maxGameTime = 3 * 10f;
    [Header("# Player info")] // Player 속성 
    public int health;
    public int maxHealth = 100;
    public int level;
    public int Kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60 , 100 ,150 ,210, 280, 360, 450, 600}; // 각 레벨에 필요한 경험치 
    
    [Header("# Game Object")] // 게임오브젝트 속성
    public PoolManager pool;
    public Player player;

    void Awake()
    {
        // 생명주기에서 인스턴스 변수를 자기자신한테 초기화
        instance = this;
    }

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if ( gameTime >  maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;

        }
    }
}

