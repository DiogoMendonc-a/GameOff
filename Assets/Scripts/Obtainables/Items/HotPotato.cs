using UnityEngine;

[CreateAssetMenu(fileName = "HotPotato", menuName = "Items/Hot Potato", order = 0)]
public class HotPotato : Item
{
	public GameObject explosion;
    public override void OnBulletHit(Vector3 position)
    {
        GameObject.Instantiate(explosion, position,Quaternion.identity);
    }
}
