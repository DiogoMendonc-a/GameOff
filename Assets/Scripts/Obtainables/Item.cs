using UnityEngine;

public class Item : Obtainable {
	public virtual void OnGetMoney(int amount) {}
	public virtual void OnBulletHit(Vector3 position) {}
	public virtual void OnPickUp() {}
}