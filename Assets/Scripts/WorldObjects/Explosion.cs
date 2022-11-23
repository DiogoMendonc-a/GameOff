using UnityEngine;

public class Explosion : MonoBehaviour {
	public int damage;
	public float range;
	public float duration;
	float time;

	public bool enemiesOnly;

	private void OnEnable() {
		time = 0;
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
		if(enemiesOnly) {
			foreach (Collider2D hit in hits)
			{
				EnemyClass ec = hit.GetComponent<EnemyClass>();
				if(ec != null) {
					ec.DealDamage(damage);
				}
			}
		}
		else {
			//TODO
		}
	}

	private void Update() {
		time += Time.deltaTime;
		if(time > duration) {
			Destroy(gameObject);
		}
	}

	private void OnDrawGizmosSelected() {
		Gizmos.DrawWireSphere(transform.position, range);	
	}
}