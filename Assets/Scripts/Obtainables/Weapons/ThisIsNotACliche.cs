using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ThisIsNotACliche", menuName = "Weapons/ThisIsNotACliche", order = 0)]
public class ThisIsNotACliche : Weapon
{
    public float cooldown = 0;
    public override void TryShoot(Vector3 position, Vector3 direction)
    {
        if (timeToReload <= 0 && currentClip > 0 && cooldown <= 0)
        {
            
            GameObject bullet_obj = GameObject.Instantiate(bullet,position , Quaternion.identity);
            bullet_obj.GetComponent<BulletClass>().Criator(PlayerClass.instance.DMG_DEAL_MULTIPLIER,range,direction,bullet_speed);
            cooldown = fireRate / PlayerClass.instance.FIRE_RATE_MULTIPLIER;
            currentClip -= 1;
        }
        else if (currentClip <= 0)
        {
            ReloadStart();
        }
    }

    public override void Update()
    {
        base.Update();
        cooldown -= Time.deltaTime;
    }
}