using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    public static PlayerClass instance;

    [HideInInspector]
    public Inventory inventory;

    public float HP = 100.0f;
    public float MOV_SPEED = 1.0f;
    
    public float DMG_DEAL_MULTIPLIER = 1.0f;
    public float DMG_RECEIVE_MULTIPLIER = 1.0f;
    
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject bullet_obj;
    
    public float atack_velocity = 10;
    public int COIN_DROP_MULTIPLIER = 1;

    void Awake() {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
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
        
        // Disparar 
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = GameObject.Instantiate(bullet_obj, transform.position, Quaternion.identity);
            

            Vector3 mouse_cords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = Vector2.zero;
            dir.x = mouse_cords.x;
            dir.y = mouse_cords.y;
            bullet.GetComponent<BulletClass>().Criator(DMG_DEAL_MULTIPLIER,1000,-dir,10);
        }
        
        
    }
}
