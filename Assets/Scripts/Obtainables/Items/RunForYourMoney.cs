using UnityEngine;

[CreateAssetMenu(fileName = "RunForYourMoney", menuName = "Items/Run For Your Money", order = 0)]
public class RunForYourMoney : Item {
	public int amount;
    public static int amountGiven;
	public override void OnPickUp()
    {
        amountGiven = amount;
    }
}