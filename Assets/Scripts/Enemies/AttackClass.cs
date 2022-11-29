using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackClass : MonoBehaviour
{
    
    public float DMG_DEAL_MULTIPLIER;
    public int DURATION = 100;
    public float SPEED = 0;

    private Vector2 direction;
    public Rigidbody2D rb;
    public Animator animator;

    
    // OnTriggerStay: Todos os frames que está lá dentro

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerClass player = other.GetComponent<PlayerClass>();
        if (player != null)
        {
            player.ChangeHp(-Mathf.FloorToInt(DMG_DEAL_MULTIPLIER));
            Destroy(gameObject);
        }
    }
    
    public void Create(float dmg,int duration, Vector2 dir, float _speed)
    {
        DMG_DEAL_MULTIPLIER = dmg;
        DURATION = duration;
        direction = dir;
        SPEED = _speed;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DURATION == 0)
        {
            Destroy(gameObject);
        }

        rb.velocity = direction.normalized * SPEED;
        DURATION -= 1;
    }
}
