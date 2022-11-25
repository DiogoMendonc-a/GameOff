using UnityEngine;

[CreateAssetMenu(fileName = "ShoestringBudget", menuName = "Items/Shoestring Budget", order = 0)]
public class ShoestringBudget : Item
{
	public float priceReduction;
	public float clipReduction;
    public override void OnPickUp()
    {
		PlayerClass.instance.CLIP_SIZE_MODIFIER *= clipReduction;
		PlayerClass.instance.MERCHANT_PRICES_MODIFIER *= priceReduction;
    }
}