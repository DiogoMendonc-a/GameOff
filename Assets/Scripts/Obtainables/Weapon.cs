using UnityEngine;
using UnityEngine.PlayerLoop;

public class Weapon : Obtainable {
	public Sprite displaySprite;
	public GameObject bullet;
	public int baseClipSize;
	[HideInInspector]
	public int currentClip;
	public float reloadTime;
	[HideInInspector]
	public float timeToReload;
	public float fireRate;
	public float range;
	public float bullet_speed;


	private bool reloading = false; 

	public virtual void TryShoot(Vector3 position, Vector3 direction) {}

	public virtual void Update()
	{
		if (reloading)
		{
			timeToReload -= Time.deltaTime;
			if (timeToReload <= 0)
			{
				ReloadEnd();
			}
		} 
	}

	public virtual void ReloadStart() {

		if (reloading)
		{
			return;
		}
		
		timeToReload = reloadTime;
		reloading = true;
		if(PlayerClass.instance.inventory.HasItem<PullAFastOne>()) {
			int roll = Random.Range(0, 100);
			if(roll < PullAFastOne.chanceToTrigger) timeToReload = 0;
		}
	}

	public virtual void ReloadEnd() {
		currentClip = Mathf.CeilToInt(baseClipSize * PlayerClass.instance.CLIP_SIZE_MODIFIER);
		Debug.Log(currentClip);
		reloading = false;
	}
	
	public void Init()
	{
		currentClip = baseClipSize;
	}
	
}