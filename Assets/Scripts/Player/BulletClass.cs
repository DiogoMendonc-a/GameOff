using UnityEngine;

public class BulletClass : MonoBehaviour
{
    
    public float DMG_DEAL_MULTIPLIER;
    public float DURATION = 100;
    public float SPEED = 0;

    protected Vector2 direction;
    public Rigidbody2D rb;
    public Animator animator;
    
    protected virtual void HandleCollision(Collider2D other) {
        EnemyClass enemie = other.GetComponent<EnemyClass>();
        if (enemie != null)
        {
            enemie.DealDamage(Mathf.CeilToInt(DMG_DEAL_MULTIPLIER));
        }

        PlayerClass player = other.GetComponent<PlayerClass>();
        if(player != null) return;
        
        PlayerClass.instance.inventory.OnBulletHit(transform.position);
        Destroy(gameObject);
      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(other);
    }
    
    public void Criator(float dmg, float range, Vector2 dir, float _speed)
    {
        DMG_DEAL_MULTIPLIER = dmg;
        SPEED = _speed * PlayerClass.instance.BULLET_SPEED_MULTIPLIER;
        DURATION = (range * PlayerClass.instance.BULLET_RANGE_MULTIPLIER) / SPEED;
        direction = dir;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void DoMovement() {
        if (DURATION <= 0)
        {
            PlayerClass.instance.inventory.OnBulletHit(transform.position);
            Destroy(gameObject);
        }

        rb.velocity = direction.normalized * SPEED;
        DURATION -= Time.deltaTime;
    }

    private void Update() {
        DoMovement();    
    }
}