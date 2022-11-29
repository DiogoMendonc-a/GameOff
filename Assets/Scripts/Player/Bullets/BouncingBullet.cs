using UnityEngine;

public class BouncingBullet : BulletClass
{
    public LayerMask layer;
    public int maxBounceNumber;

    int bounces = 0; 

    private void OnCollisionEnter2D(Collision2D other) {
        bounces++;
        Debug.Log(other.contacts[0].normal);
        direction = Vector2.Reflect(direction, other.contacts[0].normal);

        if(bounces >= maxBounceNumber) {
            PlayerClass.instance.inventory.OnBulletHit(transform.position);
            Destroy(gameObject);
        }
    }

    protected override void HandleCollision(Collider2D other) {
        EnemyClass enemie = other.GetComponent<EnemyClass>();
        if (enemie != null)
        {
            enemie.DealDamage(Mathf.CeilToInt(DMG_DEAL_MULTIPLIER));
        }

        PlayerClass player = other.GetComponent<PlayerClass>();
        if(player != null) return;
        if(((1 << other.gameObject.layer) & layer.value) != 0) {
            return;
        }

        PlayerClass.instance.inventory.OnBulletHit(transform.position);
        Destroy(gameObject);
      
    }
}
