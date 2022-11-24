using UnityEngine;

[CreateAssetMenu(fileName = "SeeingRed", menuName = "Items/Seeing Red", order = 0)]
public class SeeingRed : Item
{
	public float damageIncrease;
	public float defeseReduction;
    public override void OnPickUp()
    {
        PlayerClass.instance.DMG_DEAL_MULTIPLIER *= damageIncrease;
		PlayerClass.instance.DMG_RECEIVE_MULTIPLIER *= defeseReduction;
    }
}