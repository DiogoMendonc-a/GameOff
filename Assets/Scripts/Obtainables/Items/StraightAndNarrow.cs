using UnityEngine;

[CreateAssetMenu(fileName = "StraightAndNarrow", menuName = "Items/Straight And Narrow", order = 0)]
public class StraightAndNarrow : Item
{
	public float rangeIncrease;
	public override void OnPickUp()
    {
        PlayerClass.instance.BULLET_RANGE_MULTIPLIER *= rangeIncrease;
    }
}