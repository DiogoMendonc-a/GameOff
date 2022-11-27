using UnityEngine;

public class Weapon : Obtainable {
	public Sprite displaySprite;
	public GameObject bullet;

	public int baseClipSize;

	protected int currentClip;
	protected int maxClip;

	public float reloadTime;
	private bool reloading; 
	protected float timeToReload;
	private float initialTimeToReload;

	public float fireRate;
	protected float cooldown;

	public float range;
	public float bullet_speed;
	public float base_damage;

	public void Init()
	{
		maxClip = Mathf.CeilToInt(baseClipSize * PlayerClass.instance.CLIP_SIZE_MODIFIER);
		currentClip = maxClip;
		reloading = false;
		timeToReload = 0;
		cooldown = 0;
		InGameUIManager.instance.SetClip(currentClip, maxClip);
	}

	protected virtual void Shoot(Vector3 position, Vector3 direction, float damage) {}

	public virtual void TryShoot(Vector3 position, Vector3 direction) {
		 if (timeToReload <= 0 && currentClip > 0 && cooldown <= 0)
        {
			if(PlayerClass.instance.inventory.HasItem<BottomOfTheBarrel>() && currentClip == 0) {
				Shoot(position, direction, base_damage * PlayerClass.instance.DMG_DEAL_MULTIPLIER * BottomOfTheBarrel.multiplier);
			}
			else {	
            	Shoot(position, direction, base_damage * PlayerClass.instance.DMG_DEAL_MULTIPLIER);
			}
			cooldown = 1.0f / (fireRate * PlayerClass.instance.FIRE_RATE_MULTIPLIER);
			currentClip -= 1;
			InGameUIManager.instance.SetClip(currentClip, maxClip);
        }
        else if (currentClip <= 0)
        {
            ReloadStart();
        }
	}

	public virtual void UpdateMaxClipSize()
	{
		maxClip = Mathf.CeilToInt(baseClipSize * PlayerClass.instance.CLIP_SIZE_MODIFIER);
		InGameUIManager.instance.SetClip(currentClip, maxClip);
	}

	public virtual void Update()
	{
		if(cooldown > 0) {
			cooldown -= Time.deltaTime;
		}
		if (reloading)
		{
			timeToReload -= Time.deltaTime;
			InGameUIManager.instance.SetReloadProgress(1 - timeToReload/initialTimeToReload);
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
		
		timeToReload = reloadTime; //TODO: ADD MODIFIER
		initialTimeToReload = timeToReload;
		reloading = true;
		if(PlayerClass.instance.inventory.HasItem<PullAFastOne>()) {
			int roll = Random.Range(0, 100);
			if(roll < PullAFastOne.chanceToTrigger) timeToReload = 0;
		}
	}

	public virtual void ReloadEnd() {
		currentClip = maxClip;
		reloading = false;

		InGameUIManager.instance.SetReloadProgress(1.0f);
		InGameUIManager.instance.SetClip(currentClip, maxClip);
	}
	
}