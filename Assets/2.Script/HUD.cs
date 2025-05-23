using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, kill, Time, Health }
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length-1)];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = String.Format("Lv.{0:F0}", GameManager.instance.level);
                break;
            case InfoType.kill:
                myText.text = String.Format("{0:F0}", GameManager.instance.Kill);
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                 myText.text = String.Format("{0:D2} : {1:D2}", min, sec);  // D0,D1,D2 .... : 자리수 지정
                break;
            case InfoType.Health:
                  float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.nextExp[GameManager.instance.level];
                mySlider.value = curHealth / maxHealth;
                break;


        }
    }
}
