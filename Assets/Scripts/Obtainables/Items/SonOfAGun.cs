using UnityEngine;

[CreateAssetMenu(fileName = "SonOfAGun", menuName = "Items/Son Of A Gun", order = 0)]
public class SonOfAGun : Item
{
	public GameObject companion;
    public override void OnPickUp()
    {
		GameObject pal = GameObject.Instantiate(companion, PlayerClass.instance.transform.position, Quaternion.identity);
		pal.transform.parent = PlayerClass.instance.transform;
    }
}