using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEgg : EnemyClass
{
    public float TIMER_ATACK;
    public float MAX_TIMER_ATACK;
    public float t_move;
    public float touch_dmg;
    public float movement_time;
    public float t_atack;
    
    protected override void UpdateState()
    {
        if(HP <= 0) {
            state = MOVE_FLAG.DIE;
            return;
        }
        if(state == MOVE_FLAG.MOVE) {
            Vector3 r1 = Vector3.zero;
            TIMER_ATACK -= Time.deltaTime;
            if(TIMER_ATACK <= 0){
                state = MOVE_FLAG.ATACK;
                TIMER_ATACK = MAX_TIMER_ATACK;
                t_atack = 0;
                return;
            }
        }
    }
    protected override void DoAttackBehaviour()
    {
        if(t_atack == 0) {
            animator.CrossFade("Egg_Atack", 0, 0, 0);
        }
        t_atack += Time.deltaTime;
        if(t_atack < movement_time) {
            if(direction == null || direction == Vector2.zero) {
                direction = -PlayerClass.instance.transform.position + transform.position;
            }
            rb.velocity = direction.normalized * MOV_SPEED;
        }
        else if (t_move < 0.75f){
            rb.velocity = Vector2.zero;
        }
        else {
            GameObject ataque = GameObject.Instantiate(ataque_obj, transform.position, Quaternion.identity);
            ataque.GetComponent<AttackClass>().Create(DMG_DEAL_MULTIPLIER,10000,PlayerClass.instance.transform.position - transform.position,0);
            state = MOVE_FLAG.MOVE;
            rb.velocity = Vector2.zero;
        }
    }
    protected override void DoMoveBehaviour()
    {
        if(t_move == 0) {
            animator.CrossFade("Egg_Walk", 0, 0, 0);
        }
        t_move += Time.deltaTime;
        if(t_move < movement_time) {
            if(direction == null || direction == Vector2.zero) {
                direction = PlayerClass.instance.transform.position - transform.position;
            }
            rb.velocity = direction.normalized * MOV_SPEED;
        }
        else if (t_move < 0.75f){
            rb.velocity = Vector2.zero;
        }
        else {
            direction = Vector2.zero;
            t_move = 0;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        PlayerClass player = other.collider.GetComponent<PlayerClass>();
        if (player != null)
        {
            player.ChangeHp(-Mathf.FloorToInt(touch_dmg));
        }
    }
    
}

