using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health = 15.0f; // 현재의 체력
    public float maxHealth;// 최대의 체력
    public RuntimeAnimatorController[] animCon; 
    public Rigidbody2D target;
    Rigidbody2D rb;
    SpriteRenderer spriter;
    Animator anim;
    Collider2D coll;
    WaitForFixedUpdate wait;
    bool isLive;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();  
    }

    void FixedUpdate()
    {
          if (!GameManager.instance.isLive)
            return;

        if (!isLive)
            return;

        Vector2 dirVec = target.position - rb.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
        rb.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
          if (!GameManager.instance.isLive)
            return;
            
         if (!isLive)
            return;

        spriter.flipX = target.position.x < rb.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;  

        if(health > 0)
        {
 
        }
        else
        {
           Dead();
           GameManager.instance.Kill++;
           GameManager.instance.GetExp();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
  
