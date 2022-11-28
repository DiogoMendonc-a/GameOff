using UnityEngine;

public class SonOfAGunCompanion : MonoBehaviour {
	Vector3 targetPos;
	public float range;
	public float fireTime;
	public float moveTime;
	public float moveSpeed;
	public float bulletSpeed;
	public int damage;
	public GameObject bullet;

	GameObject target;

	private void Update() {
		DoMovement();
		DoGetTarget();
		DoAim();
		DoShooting();
	}

	float mtime = 0;
	void DoMovement() {
		mtime -= Time.deltaTime;
		if(mtime <= 0) {
			targetPos = ERandom.GetRandomPlanarVector();
			mtime = moveTime;
		}

		transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, moveSpeed * Time.deltaTime);
	}

	void DoGetTarget() {
		if(target != null) {
			if(Vector3.Distance(target.transform.position, transform.position) < range) return;
		}

		float distance = float.MaxValue;
		target = null;
		
		RaycastHit2D[] hits = Physics2D.CircleCastAll(PlayerClass.instance.transform.position, range * PlayerClass.instance.BULLET_RANGE_MULTIPLIER, Vector2.up);
		foreach (RaycastHit2D hit in hits)
		{
			EnemyClass enemy = hit.collider.gameObject.GetComponent<EnemyClass>();
			if(enemy != null) {
				if(target == null) {
					target = enemy.gameObject;
				}
				else {
					float d = Vector3.Distance(enemy.transform.position, transform.position);
					if(d < distance) {
						distance = d;
						target = enemy.gameObject;
					}
				}
			}
		}

	}

	void DoAim() {
		if(target == null) return;

		transform.LookAt(transform.position + Vector3.forward, target.transform.position - transform.position);		
	}

	float stime = 0;
	void DoShooting() {
		stime -= Time.deltaTime;
		if(target == null) return;

		if(stime <= 0) {
			GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
			b.GetComponent<BulletClass>().Criator(damage * PlayerClass.instance.DMG_DEAL_MULTIPLIER, range, transform.up, bulletSpeed); //TODO: Range should go as float
			stime = fireTime;
		}
	}
}