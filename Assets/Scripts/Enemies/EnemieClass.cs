using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemieClass : MonoBehaviour
{

    public float HP = 100.0f;
    public float MOV_SPEED = 1.0f;
    
    public float DMG_DEAL_MULTIPLIER = 1.0f;
    public float DMG_RECEIVE_MULTIPLIER = 1.0f;

    public Animator animator;
    public Rigidbody2D rb;
    public GameObject ataque_obj;

    // Atack counter
    public int atack_frame_counter = 0;
    public int FRAMES_BETWEEN_ATACK = 1000;

    public Vector2 direction = Vector2.zero;

    public float atack_velocity = 10;
    public int COIN_DROP = 0;
    
    // Move flag
    // -1 -> Inimigo está inativo
    // 0 -> Inimigo é estático
    // 1 -> Inimigo move-se
    
    public enum MOVE_FLAG 
    {   
        INACTIVE,
        STATIC,
        MOVE,
        ATACK,
        DEFENSE,
        DIE
    }

    public MOVE_FLAG state = MOVE_FLAG.INACTIVE;


    // Start is called before the first frame update

    void InitilizeEnemie()
    {
        return;
    }
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        InitilizeEnemie();
    }
    
    
    // state machine
    Vector2 StateControler()
    {
        if (state == MOVE_FLAG.INACTIVE)
        {
            return Vector2.zero;
        }
        
        // Movement
        if (state == MOVE_FLAG.MOVE)
        {
            Vector2 pos = direction - rb.position ;
            pos = pos.normalized * MOV_SPEED;
            
            // Isto serve para ele não andar de um lado para o outro
            if (Mathf.Abs(pos.x) + Mathf.Abs(pos.y) < 0.8)
            {
                return Vector2.zero;
            }
            
            return new Vector2(pos.x,pos.y);
        }

        if (state == MOVE_FLAG.STATIC)
        {
            return Vector2.zero;
        }

        if (state == MOVE_FLAG.ATACK)
        {
            GameObject ataque = GameObject.Instantiate(ataque_obj, transform.position, Quaternion.identity);
            
            // TO DO : change direction
            Vector2 rando = new Vector2();
            rando.x = Random.Range(-10, 10);
            rando.y = Random.Range(-10, 10);
            
            ataque.GetComponent<AtackClass>().Criator(DMG_DEAL_MULTIPLIER,1000,rando,10);
            state = MOVE_FLAG.MOVE;
            return Vector2.zero;
        }

        if (state == MOVE_FLAG.DEFENSE)
        {
            return Vector2.zero;
        }

        return Vector2.zero;
    }
    
    // Update is called once per frame
    void Update()
    {
        atack_frame_counter += 1;
        if (atack_frame_counter == FRAMES_BETWEEN_ATACK)
        {
            state = MOVE_FLAG.ATACK;
            atack_frame_counter = 0;
        }
        
        
        if (HP <= 0)
        {
            state = MOVE_FLAG.DIE;
            
            // Need this to not revive;
            atack_frame_counter = FRAMES_BETWEEN_ATACK + 10;
        }
        
        rb.velocity = StateControler();
        if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
