using UnityEngine;

[CreateAssetMenu(fileName = "SilverBullet", menuName = "Items/Silver Bullet", order = 0)]
public class SilverBullet : Item {
	public float damageMultiplier;
    public static float multiplier = 1.0f;
	public override void OnPickUp()
    {
        multiplier *= damageMultiplier;
    }
}