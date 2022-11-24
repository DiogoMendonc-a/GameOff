using UnityEngine;

[CreateAssetMenu(fileName = "PullAFastOne", menuName = "Items/Pull A Fast One", order = 0)]
public class PullAFastOne : Item {
	[Range(0, 100)]
	public int chance;
    public static int chanceToTrigger;
	public override void OnPickUp()
    {
        chanceToTrigger = chance;
    }
}