using UnityEngine;
using System.Collections;

public class PlayerClass : MonoBehaviour
{
    public static PlayerClass instance;

    [HideInInspector]
    public Inventory inventory;

    public Weapon default_weapon;
    
    public float MAX_HP = 100.0f;
    public float CURRENT_HP;
    public float MOV_SPEED = 1.0f;
    
    public float DMG_DEAL_MULTIPLIER = 1.0f;
    public float DMG_RECEIVE_MULTIPLIER = 1.0f;
    public float BULLET_RANGE_MULTIPLIER = 1.0f;
    public float BULLET_SPEED_MULTIPLIER = 1.0f;
    public float FIRE_RATE_MULTIPLIER = 1.0f;
    private float _clip_size_modifier = 1.0f;
    public float CLIP_SIZE_MODIFIER {
        get {
            return _clip_size_modifier;
        }
        set {
            _clip_size_modifier = value;
            inventory.weapon.UpdateMaxClipSize();
        }
    }

    public float MERCHANT_PRICES_MODIFIER = 1.0f;
    
    public Animator animator;
    public Rigidbody2D rb;

    //dash 
    private Vector2 dash_dir;
    private bool can_dash = true;
    private bool is_dashing;
    public float dash_power = 10f;
    public float dash_time = 0.2f;
    public float dash_cooldown = 1f;
    public TrailRenderer tr; 

    void Awake() {
        instance = this;
        CURRENT_HP = MAX_HP;
    }


    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inventory.SetWeapon(default_weapon);
    }

    public void ChangeHp(int value) {
        CURRENT_HP += value;
        CURRENT_HP = Mathf.Clamp(CURRENT_HP, 0, MAX_HP);

        InGameUIManager.instance.SetHealth(CURRENT_HP/MAX_HP);
    } 
    
    public void ChangeMaxHp(int value) {
        MAX_HP += value;
        CURRENT_HP = Mathf.Clamp(CURRENT_HP, 0, MAX_HP);

        InGameUIManager.instance.SetHealth(CURRENT_HP/MAX_HP);
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
    
    private IEnumerator Dash()
    {
        can_dash = false;
        is_dashing = true;
        dash_dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        tr.emitting = true;
        rb.velocity = dash_dir.normalized * dash_power;
        yield return new WaitForSeconds(dash_time);
        tr.emitting = false;
        is_dashing = false;
        yield return new WaitForSeconds(dash_cooldown);
        can_dash = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(is_dashing || InGameUIManager.instance.openMenu)
        {
            return;
        }

        rb.velocity = MovementControler();
        FlipControler();

        if (CURRENT_HP <= 0)
        {
            if(PlayerClass.instance.inventory.HasItem<SecondWind>() && !SecondWind.used) {
                MAX_HP = MAX_HP * SecondWind.percentage;
                CURRENT_HP = MAX_HP * SecondWind.percentage;
                InGameUIManager.instance.SetHealth(CURRENT_HP/MAX_HP);
                SecondWind.used = true;
            }
            else {
                GameManager.instance.LoseGame();
            }
        }
        
        // Disparar 
        if (Input.GetMouseButton(0))
        {
            
            Vector3 mouse_cords = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Vector2 dir = Vector2.zero;
            dir.x = mouse_cords.x;
            dir.y = mouse_cords.y;
            if(inventory.HasItem<HorsingAround>()) {
                dir = ERandom.GetRandomPlanarVector();
            }
            
            
            inventory.weapon.TryShoot(inventory.weaponRenderer.transform.position, dir);
            
        }
        
        // Dash
        if (Input.GetKey(KeyCode.Space) && can_dash)
        {
            StartCoroutine(Dash());
        }     
    }
}
