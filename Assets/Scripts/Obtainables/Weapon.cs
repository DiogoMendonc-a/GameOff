using UnityEngine;

public class Weapon : Obtainable {
	public int baseClipSize;
	int currentClip;
	public float reloadTime;
	float timeToReload;
	public virtual void TryShoot(Vector3 position, Vector3 direction) {}

	public virtual void ReloadStart() {
		timeToReload = reloadTime;
	}

	public virtual void ReloadEnd() {
		currentClip = Mathf.CeilToInt(baseClipSize * PlayerClass.instance.CLIP_SIZE_MODIFIER);
	}
}