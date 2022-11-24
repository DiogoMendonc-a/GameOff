using UnityEngine;

[CreateAssetMenu(fileName = "SecondWind", menuName = "Items/Second Wind", order = 0)]
public class SecondWind : Item {
	public float modifier;
    public static float percentage;
    public static bool used = false;
	public override void OnPickUp()
    {
        percentage = modifier;
    }
}