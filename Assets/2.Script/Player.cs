using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 vector2;
    public float speed;
    public Scanner scanner;
    Rigidbody2D rb;
    SpriteRenderer spriter;
    Animator anim;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    
    void Update()
    {
        vector2.x = Input.GetAxisRaw("Horizontal");
        vector2.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 nextVec = vector2.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }

    void LateUpdate()
    {
        anim.SetFloat("Speed", vector2.magnitude);
        if (vector2.x != 0)
        {
            spriter.flipX = vector2.x < 0;
        }
    }
}
