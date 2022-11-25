using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ThisIsNotACliche", menuName = "Weapons/ThisIsNotACliche", order = 0)]
public class ThisIsNotACliche : Weapon
{
    public float cooldown = 0;
    public override void TryShoot(Vector3 position, Vector3 direction)
    {
        if (timeToReload == 0 && currentClip > 0 && cooldown == 0)
        {
            cooldown = fireRate / PlayerClass.instance.FIRE_RATE_MULTIPLIER;
            currentClip -= 1;
        }
    }
}