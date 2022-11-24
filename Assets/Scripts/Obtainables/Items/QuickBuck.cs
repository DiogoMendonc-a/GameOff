using UnityEngine;

[CreateAssetMenu(fileName = "QuickBuck", menuName = "Items/Quick Buck", order = 0)]
public class QuickBuck : Item {
	public float moneyMultiplier;
    public static float multiplier;
    public static bool triggered = false;
	public override void OnPickUp()
    {
        multiplier *= moneyMultiplier;
    }
}