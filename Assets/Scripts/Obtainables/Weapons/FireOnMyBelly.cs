using UnityEngine;

[CreateAssetMenu(fileName = "FireOnMyBelly", menuName = "Weapons/Fire On My Belly", order = 0)]
public class FireOnMyBelly : Weapon
{
	public int bullet_number;
	public float arc;

    protected override void Shoot(Vector3 position, Vector3 direction, float damage)
    {
		for(int i = 0; i < bullet_number * PlayerClass.instance.FIRE_RATE_MULTIPLIER; i++) {
			float angle = Random.Range(-(arc * PlayerClass.instance.BULLET_RANGE_MULTIPLIER)/2, (arc * PlayerClass.instance.BULLET_RANGE_MULTIPLIER)/2);
			Vector3 bullet_direction = Quaternion.AngleAxis(angle, Vector3.forward) * direction;
			GameObject bullet_obj = GameObject.Instantiate(bullet, position, Quaternion.identity);
			bullet_obj.GetComponent<BulletClass>().Criator(damage, range, bullet_direction, bullet_speed);
		}
    }
}