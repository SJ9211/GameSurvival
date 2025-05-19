using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")] // GameControl 속성
    public bool isLive;
    public float gameTime; // 흐르는 시간
    public float maxGameTime = 2 * 10f;

    [Header("# Player info")] // Player 속성 
    public float health;
    public float maxHealth = 100;
    public int level;
    public int Kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 }; // 각 레벨에 필요한 경험치 

    [Header("# Game Object")] // 게임오브젝트 속성
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;

    void Awake()
    {
        // 생명주기에서 인스턴스 변수를 자기자신한테 초기화
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;

        // 임시 스크립트 (첫번째 캐릭터 선택)
        uiLevelUp.Select(0);
        Resume();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }

        public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (!isLive)
            return;
            
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive)
            return;
            
        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()  // 시간정지 함수
    {
        isLive = false;
        Time.timeScale = 0; // 유니티의 시간속도(배율)

    }
    
    public void Resume() // 시간작동 함수
    {
        isLive = true;
        Time.timeScale = 1;
    }
}

