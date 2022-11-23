using UnityEngine;

[CreateAssetMenu(fileName = "PennySavedPennyEarned", menuName = "Items/Penny Saved Penny Earned", order = 0)]
public class PennySavedPennyEarned : Item
{
    public override void OnGetMoney(int amount)
    {
        PlayerClass.instance.inventory.AddMoney(amount);
    }
}
