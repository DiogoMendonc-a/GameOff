using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    
    public float HP = 100.0f;
    public float MOV_SPEED = 1.0f;
    
    public float DMG_DEAL_MULTIPLIER = 1.0f;
    public float DMG_RECEIVE_MULTIPLIER = 1.0f;
    
    public Animator animator;
    public Rigidbody2D rb;
    
    public float atack_velocity = 10;
    public int COIN_DROP_MULTIPLIER = 1;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    Vector2 MovementControler()
    {
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            dir.x -= MOV_SPEED;
        }

        if (Input.GetKey(KeyCode.D))
        {
            dir.x += MOV_SPEED;
        }

        if (Input.GetKey(KeyCode.S))
        {
            dir.y -= MOV_SPEED;
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y += MOV_SPEED;
        }

        return dir;
    }
    
    void FlipControler()
    {
        if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        rb.velocity = MovementControler();
        FlipControler();

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
