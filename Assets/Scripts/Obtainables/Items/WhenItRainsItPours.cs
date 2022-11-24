using UnityEngine;

[CreateAssetMenu(fileName = "WhenItRainsItPours", menuName = "Items/When It Rains It Pours", order = 0)]
public class WhenItRainsItPours : Item {
	public float clipSizeIncrease;
    
	public override void OnPickUp()
    {
        PlayerClass.instance.CLIP_SIZE_MODIFIER += clipSizeIncrease;
    }
}