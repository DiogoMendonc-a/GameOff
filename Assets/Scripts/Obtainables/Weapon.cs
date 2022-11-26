using UnityEngine;

public class Weapon : Obtainable {
	public Sprite displaySprite;
	public GameObject bullet;

	public int baseClipSize;

	protected int currentClip;

	public float reloadTime;
	private bool reloading; 
	protected float timeToReload;

	public float fireRate;
	protected float cooldown;

	public float range;
	public float bullet_speed;
	public float base_damage;

	public void Init()
	{
		currentClip = baseClipSize;
		reloading = false;
		timeToReload = 0;
		cooldown = 0;
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
        }
        else if (currentClip <= 0)
        {
            ReloadStart();
        }
	}

	public virtual void Update()
	{
		if(cooldown > 0) {
			cooldown -= Time.deltaTime;
		}
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
	
}