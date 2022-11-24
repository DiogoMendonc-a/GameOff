using UnityEngine;

[CreateAssetMenu(fileName = "HorsingAround", menuName = "Items/Horsing Around", order = 0)]
public class HorsingAround : Item
{
	public float fireRateIncrease;
    public override void OnPickUp()
    {
        PlayerClass.instance.FIRE_RATE_MULTIPLIER += fireRateIncrease;
    }
}
