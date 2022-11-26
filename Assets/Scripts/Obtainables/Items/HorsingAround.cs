using UnityEngine;

[CreateAssetMenu(fileName = "HorsingAround", menuName = "Items/Horsing Around", order = 0)]
public class HorsingAround : Item
{
	public float clipIncrease;
	public float fireRateIncrease;
    public override void OnPickUp()
    {
        PlayerClass.instance.FIRE_RATE_MULTIPLIER *= fireRateIncrease;
        PlayerClass.instance.CLIP_SIZE_MODIFIER *= clipIncrease;
    }
}
