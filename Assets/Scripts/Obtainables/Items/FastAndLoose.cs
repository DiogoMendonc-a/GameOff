using UnityEngine;

[CreateAssetMenu(fileName = "FastAndLoose", menuName = "Items/Fast And Loose", order = 0)]
public class FastAndLoose : Item
{
	public float movSpeedIncrease;
	public float fireRateIncrease;
    public override void OnPickUp()
    {
        PlayerClass.instance.MOV_SPEED *= movSpeedIncrease;
        PlayerClass.instance.FIRE_RATE_MULTIPLIER *= fireRateIncrease;
    }
}
