using UnityEngine;

public class Weapon : Obtainable {
	public Sprite displaySprite;
	public int baseClipSize;
	[HideInInspector]
	public int currentClip;
	public float reloadTime;
	[HideInInspector]
	public float timeToReload;
	public float fireRate;
	public virtual void TryShoot(Vector3 position, Vector3 direction) {}
	
	public virtual void ReloadStart() {
		timeToReload = reloadTime;
		if(PlayerClass.instance.inventory.HasItem<PullAFastOne>()) {
			int roll = Random.Range(0, 100);
			if(roll < PullAFastOne.chanceToTrigger) timeToReload = 0;
			
		}
	}

	public virtual void ReloadEnd() {
		currentClip = Mathf.CeilToInt(baseClipSize * PlayerClass.instance.CLIP_SIZE_MODIFIER);
	}
}