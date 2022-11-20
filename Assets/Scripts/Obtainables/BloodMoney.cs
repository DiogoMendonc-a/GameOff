using UnityEngine;

[CreateAssetMenu(fileName = "BloodMoney", menuName = "Items/Blood Money", order = 0)]
public class BloodMoney : Item {
	public float range;
	public int damage;

	public override void OnGetMoney(int amount) {
		RaycastHit2D[] hits = Physics2D.CircleCastAll(PlayerClass.instance.transform.position, range, Vector2.up);
		foreach (RaycastHit2D hit in hits)
		{
			EnemyClass enemy = hit.collider.gameObject.GetComponent<EnemyClass>();
			if(enemy != null) {
				enemy.DealDamage(damage);
			}
		}
	}
}