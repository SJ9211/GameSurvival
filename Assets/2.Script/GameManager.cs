using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float gameTime; // 흐르는 시간
    public float maxGameTime = 2 * 10f;
    public PoolManager pool;

    public Player player;

    void Awake()
    {
        // 생명주기에서 인스턴스 변수를 자기자신한테 초기화
        instance = this;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if ( gameTime >  maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
    }

