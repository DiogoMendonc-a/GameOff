using UnityEngine;

[CreateAssetMenu(fileName = "GlassHousesThrowStones", menuName = "Items/Glass Houses Throw Stones", order = 0)]
public class GlassHousesThrowStones : Item
{
	public float damageIncrease;
	public int hpDecrease;
    public override void OnPickUp()
    {
        PlayerClass.instance.DMG_DEAL_MULTIPLIER *= damageIncrease;
        PlayerClass.instance.ChangeMaxHp(-hpDecrease);
    }
}
