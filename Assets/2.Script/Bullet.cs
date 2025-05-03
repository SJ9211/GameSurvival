using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 5.0f;
    public int per;
    
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if ( per > -1)
        {
            rb.velocity = dir * 15.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) // 관통에 대한 내용
    {
        if(!collision.CompareTag("Enemy") || per == -1)
            return;

        per--;

        if(per == -1)
        {
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

}

