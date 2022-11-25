using UnityEngine;

[CreateAssetMenu(fileName = "StickToYourGun", menuName = "Items/Stick To Your Gun", order = 0)]
public class StickToYourGun : Item
{
	public float damageIncrease;
	public float rangeIncrease;
	public float rateIncrease;
	public override void OnPickUp()
    {
        PlayerClass.instance.DMG_DEAL_MULTIPLIER *= damageIncrease;
        PlayerClass.instance.BULLET_RANGE_MULTIPLIER *= rangeIncrease;
        PlayerClass.instance.FIRE_RATE_MULTIPLIER *= rateIncrease;
    }
}