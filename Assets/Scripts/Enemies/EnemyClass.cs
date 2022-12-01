using UnityEngine;
using System;

public class EnemyClass : MonoBehaviour
{
    public bool isBoss = false;
    public bool dead = false;
    public float HP = 100.0f;
    public float MOV_SPEED = 1.0f;
    
    public float DMG_DEAL_MULTIPLIER = 1.0f;
    public float DMG_RECEIVE_MULTIPLIER = 1.0f;

    public Animator animator;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public GameObject ataque_obj;

    // Atack counter
    public int atack_frame_counter = 0;
    public int FRAMES_BETWEEN_ATACK = 1000;

    public Vector2 direction = Vector2.zero;

    public float atack_velocity = 10;
    public int COIN_DROP = 0;

    public Action OnDie;

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
        DIE,
        TAKE_DMG
    }

    public MOVE_FLAG state = MOVE_FLAG.INACTIVE;


    // Start is called before the first frame update

    protected virtual void InitiliazeEnemy()
    {
        return;
    }
    
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        InitiliazeEnemy();
    }

    public virtual void OnReceiveDamage(int value) {
        if(state == MOVE_FLAG.INACTIVE)
            state = MOVE_FLAG.MOVE;
    }
    
    public virtual void DealDamage(int value) {
        HP -= value;
        OnReceiveDamage(value);
    }

    public virtual void ReceiveDMG()
    {
        return;
        //state = MOVE_FLAG.TAKE_DMG;
    }
    
    protected virtual void UpdateState() {
        if (state != MOVE_FLAG.INACTIVE && state != MOVE_FLAG.DIE)
        {
            atack_frame_counter += 1;
        }

        if (atack_frame_counter == FRAMES_BETWEEN_ATACK)
        {
            state = MOVE_FLAG.ATACK;
            atack_frame_counter = 0;
        }
        
        
        if (HP <= 0)
        {
            state = MOVE_FLAG.DIE;
        }
    }
    
    // state machine
    void ExecuteState()
    {
        if (state == MOVE_FLAG.INACTIVE)
        {
            DoInactiveBehaviour();
        }
        
        if (state == MOVE_FLAG.MOVE)
        {
            DoMoveBehaviour();
        }

        if (state == MOVE_FLAG.STATIC)
        {
            DoStaticBehaviour();
        }

        if (state == MOVE_FLAG.ATACK)
        {
            DoAttackBehaviour();
        }

        if (state == MOVE_FLAG.DEFENSE)
        {
            DoDefenseBehaviour();
        }

        if (state == MOVE_FLAG.DIE)
        {
            DieAnimation();
        }

        if (dead)
        {
            DoDieBehaviour();
        }
        
    }

    public virtual void DieAnimation()
    {
        dead = true;
    }
    
    // Update is called once per frame
    public virtual void Update()
    {
        UpdateState();
        
        ExecuteState();

        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
    }

    protected virtual void DoInactiveBehaviour() {
        rb.velocity = Vector2.zero;
    }

    protected virtual void DoMoveBehaviour() {
        Vector2 pos = direction - rb.position ;
        pos = pos.normalized * MOV_SPEED;
        
        // Isto serve para ele não andar de um lado para o outro
        if (Mathf.Abs(pos.x) + Mathf.Abs(pos.y) < 0.8)
        {
            rb.velocity =  Vector2.zero;
        }
        
        rb.velocity =  new Vector2(pos.x,pos.y);
    }

    protected virtual void DoStaticBehaviour() {
        rb.velocity = Vector2.zero;
    }

    protected virtual void DoAttackBehaviour() {
        GameObject ataque = GameObject.Instantiate(ataque_obj, transform.position, Quaternion.identity);
            
        // TO DO : change direction
        Vector2 rando = new Vector2();
        rando.x = UnityEngine.Random.Range(-10, 10);
        rando.y = UnityEngine.Random.Range(-10, 10);
        
        ataque.GetComponent<AttackClass>().Create(DMG_DEAL_MULTIPLIER,1000,rando,10);
        state = MOVE_FLAG.MOVE;
        rb.velocity = Vector2.zero;
    } 

    protected virtual void DoDefenseBehaviour() {
        rb.velocity = Vector2.zero;
    }

    protected virtual void DoDieBehaviour() {
        if(PlayerClass.instance.inventory.HasItem<SurvivalOfTheFittest>()) {
            PlayerClass.instance.ChangeHp(SurvivalOfTheFittest.healAmount);
        }

        int numberToDrop = COIN_DROP;
        if(PlayerClass.instance.inventory.HasItem<QuickBuck>() && !QuickBuck.triggered){
            numberToDrop *= Mathf.CeilToInt(QuickBuck.multiplier);
            QuickBuck.triggered = true;
        }
        for (int i = 0; i < numberToDrop; i++)
        {
            Vector3 randomPos = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 0);
            GameObject.Instantiate(ResourcesManager.instance.moneyObj, this.transform.position + randomPos, Quaternion.identity);
        }

        if(OnDie != null) OnDie.Invoke();
        Destroy(gameObject);
    }
}
