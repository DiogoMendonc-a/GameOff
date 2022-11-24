using UnityEngine;

[CreateAssetMenu(fileName = "ScotFree", menuName = "Items/Scot Free", order = 0)]
public class ScotFree : Item
{
	public float movSpeedIncrease;
	public float damageReduction;
    public override void OnPickUp()
    {
		PlayerClass.instance.MOV_SPEED += movSpeedIncrease;
		PlayerClass.instance.DMG_RECEIVE_MULTIPLIER *= damageReduction;
    }
}