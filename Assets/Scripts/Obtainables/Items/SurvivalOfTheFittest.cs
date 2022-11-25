using UnityEngine;

[CreateAssetMenu(fileName = "SurvivalOfTheFittest", menuName = "Items/Survival Of The Fittest", order = 0)]
public class SurvivalOfTheFittest : Item {
	public int amount;
    public static int healAmount;
	public override void OnPickUp()
    {
        healAmount = amount;
    }
}