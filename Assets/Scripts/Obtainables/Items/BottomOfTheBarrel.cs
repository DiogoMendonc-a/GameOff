using UnityEngine;

[CreateAssetMenu(fileName = "BottomOfTheBarrel", menuName = "Items/Bottom Of The Barrel", order = 0)]
public class BottomOfTheBarrel : Item {
	public float damageMultiplier;
    public static float multiplier;
	public override void OnPickUp()
    {
        multiplier *= damageMultiplier;
    }
}