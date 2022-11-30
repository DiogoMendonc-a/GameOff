using UnityEngine;

public class StayingBullet : BulletClass
{
	public float timeToLay;
	bool laying = false;
    float timeLaying = 0;
    protected override void DoMovement() {
		if (DURATION <= 0)
        {
			laying = true;
        }

		if(laying) {
            rb.velocity = Vector2.zero;
            timeLaying += Time.deltaTime;
            if(timeLaying >=  timeToLay) {
                PlayerClass.instance.inventory.OnBulletHit(transform.position);
                Destroy(gameObject);
            }
			return;
		}

		rb.velocity = direction.normalized * SPEED;
		DURATION -= Time.deltaTime;

    }
}
