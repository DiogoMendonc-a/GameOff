using UnityEngine;

public class BulletClass : MonoBehaviour
{
    
    public float DMG_DEAL_MULTIPLIER;
    public float DURATION = 100;
    public float SPEED = 0;

    private Vector2 direction;
    public Rigidbody2D rb;
    public Animator animator;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyClass enemie = other.GetComponent<EnemyClass>();
        if (enemie != null)
        {
            enemie.HP -= 1;
        }

        PlayerClass player = other.GetComponent<PlayerClass>();
        if(player != null) return;
        
        PlayerClass.instance.inventory.OnBulletHit(transform.position);
        Destroy(gameObject);
    }
    
    public void Criator(float dmg,float duration, Vector2 dir, float _speed)
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
        if (DURATION <= 0)
        {
            PlayerClass.instance.inventory.OnBulletHit(transform.position);
            Destroy(gameObject);
        }

        rb.velocity = direction.normalized * SPEED;
        DURATION -= Time.deltaTime;
    }
}