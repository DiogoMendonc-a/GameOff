using UnityEngine;

[CreateAssetMenu(fileName = "SafeAndSound", menuName = "Items/Safe And Sound", order = 0)]
public class SafeAndSound : Item
{
	public int MaxHpIncrease;
	public float damageReduction;
    public override void OnPickUp()
    {
        PlayerClass.instance.MAX_HP += MaxHpIncrease;
        PlayerClass.instance.ChangeHp(MaxHpIncrease);
        PlayerClass.instance.ChangeMaxHp(MaxHpIncrease);
		PlayerClass.instance.DMG_RECEIVE_MULTIPLIER *= damageReduction;
    }
}