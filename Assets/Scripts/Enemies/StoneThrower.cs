using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneThrower : Boss
{

    public float collisionDMG;
    public float t_move;
    public float movement_time;
    public float t_atack;
    public float TIMER_ATACK;
    public float MAX_TIMER_ATACK;
    public bool taking_dmg = false;
    public float fsolong = 1.3f;
    private bool help = true;
    public float invencibility = 0;
    
    
    protected override void UpdateState()
    {
        invencibility -= Time.deltaTime;
        if (taking_dmg)
        {
            fsolong -= Time.deltaTime;
            if (fsolong <= 0)
            {
                taking_dmg = false;
                state = MOVE_FLAG.MOVE;
            }
        }
        if (state == MOVE_FLAG.TAKE_DMG && !taking_dmg)
        {
            taking_dmg = true;
            animator.CrossFade("Stone_ReceiveDMG", 0, 0, 0);
            fsolong = 1.3f;
        } 
        
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
    
        protected void DoCure()
    {
        if(t_atack == 0) {
            animator.CrossFade("Stone_ReceiveHP", 0, 0, 0);
            rb.velocity = Vector2.zero;
        }
        t_atack += Time.deltaTime;
        if(t_atack < movement_time) {
            if(direction == null || direction == Vector2.zero) {
                direction = -PlayerClass.instance.transform.position + transform.position;
            }
            rb.velocity = direction.normalized * MOV_SPEED;
        }
        else if (t_move < 0.60f){
            rb.velocity = Vector2.zero;
        }
        else
        {
            HP += 100;
            state = MOVE_FLAG.MOVE;
            rb.velocity = Vector2.zero;
        }
    }

        public override void ReceiveDMG()
        {
            if (invencibility <= 0)
            {
                state = MOVE_FLAG.TAKE_DMG;
                invencibility = 10;
            }


        }

        protected override void DoAttackBehaviour()
    {

        if (Random.Range(0,2) > 2.8 && help)
        {
            DoCure();
            return;
        }

        help = false;
        
        if(t_atack == 0) {
            animator.CrossFade("Stone_Atack", 0, 0, 0);
            rb.velocity = Vector2.zero;
        }
        t_atack += Time.deltaTime;
        if(t_atack < movement_time) {
            if(direction == null || direction == Vector2.zero) {
                direction = -PlayerClass.instance.transform.position + transform.position;
            }
            rb.velocity = direction.normalized * MOV_SPEED;
        }
        else if (t_move < 0.60f){
            rb.velocity = Vector2.zero;
        }
        else {
            Debug.Log("AQUI");
            GameObject ataque = GameObject.Instantiate(ataque_obj, transform.position, Quaternion.identity);
            GameObject ataque2 = GameObject.Instantiate(ataque_obj, transform.position, Quaternion.identity);
            GameObject ataque3 = GameObject.Instantiate(ataque_obj, transform.position, Quaternion.identity);
            GameObject ataque4 = GameObject.Instantiate(ataque_obj, transform.position, Quaternion.identity);
            GameObject ataque5 = GameObject.Instantiate(ataque_obj, transform.position, Quaternion.identity);
            
            if (Random.Range(0, 2) > 0.4)
            {
                ataque.GetComponent<AttackClass>().Create(DMG_DEAL_MULTIPLIER, 3000,
                    PlayerClass.instance.transform.position - transform.position + Vector3.up, 4);
                ataque2.GetComponent<AttackClass>().Create(DMG_DEAL_MULTIPLIER, 3000,
                    PlayerClass.instance.transform.position - transform.position + Vector3.down, 4);
            }
            
            if (Random.Range(0, 2) > 0.4)
            {
                ataque3.GetComponent<AttackClass>().Create(DMG_DEAL_MULTIPLIER, 3000,
                    PlayerClass.instance.transform.position - transform.position + Vector3.left, 4);
                ataque4.GetComponent<AttackClass>().Create(DMG_DEAL_MULTIPLIER, 3000,
                    PlayerClass.instance.transform.position - transform.position + Vector3.right, 4);
            }
            
            ataque5.GetComponent<AttackClass>().Create(DMG_DEAL_MULTIPLIER,3000,PlayerClass.instance.transform.position - transform.position,4);
            state = MOVE_FLAG.MOVE;
            rb.velocity = Vector2.zero;
            help = true;
            Debug.Log("AQUII");
        }
    }
    
    protected override void DoMoveBehaviour()
    {
        if (taking_dmg)
        {
            fsolong -= Time.deltaTime;
            if (fsolong < 0)
            {
                taking_dmg = false;
            }
        }
        if(t_move == 0 && !taking_dmg) {
            animator.CrossFade("StoneWalk", 0, 0, 0);
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
            player.ChangeHp(-Mathf.FloorToInt(collisionDMG));
        }
    }
}
