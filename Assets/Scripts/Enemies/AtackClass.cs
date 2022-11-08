using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackClass : MonoBehaviour
{
    
    public float DMG_DEAL_MULTIPLIER;
    public int DURATION = 100;

    private Vector2 direction;
    public Rigidbody2D rb;
    public Animator animator;


    public void Criator(float dmg,int duration, Vector2 dir)
    {
        DMG_DEAL_MULTIPLIER = dmg;
        DURATION = duration;
        direction = dir;
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

        rb.velocity = -direction;
        DURATION -= 1;
    }
}
