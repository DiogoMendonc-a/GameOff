using UnityEngine;

[CreateAssetMenu(fileName = "SmallWorld", menuName = "Items/Small World", order = 0)]
public class SmallWorld : Item
{
	public float rangeIncrease;
	public float speedIncrease;
	public override void OnPickUp()
    {
        PlayerClass.instance.BULLET_RANGE_MULTIPLIER *= rangeIncrease;
        PlayerClass.instance.BULLET_SPEED_MULTIPLIER *= speedIncrease;
    }
}