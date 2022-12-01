using UnityEngine;

public class BoomerandBullet : BulletClass
{
	public float returnDamageModifier;
	bool returning = false;

    protected override void HandleCollision(Collider2D other) {
        EnemyClass enemie = other.GetComponent<EnemyClass>();
        if (enemie != null)
        {
			if(returning)
            	enemie.DealDamage(Mathf.CeilToInt(returnDamageModifier * DMG_DEAL_MULTIPLIER));
            else
				enemie.DealDamage(Mathf.CeilToInt(DMG_DEAL_MULTIPLIER));
        }

        PlayerClass player = other.GetComponent<PlayerClass>();
        if(player != null && returning) {
        	Destroy(gameObject);
			return;
		}

		if(player != null) return;
		
        PlayerClass.instance.inventory.OnBulletHit(transform.position);
        if(returning) {
			Destroy(gameObject);
		}
		else {
			returning = true;
		}
      
    }


    protected override void DoMovement() {
		if (DURATION <= 0)
        {
			returning = true;
        }

		if(returning) {
			direction = PlayerClass.instance.transform.position - transform.position;
		}

		rb.velocity = direction.normalized * SPEED;
		DURATION -= Time.deltaTime;

    }
}
