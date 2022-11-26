using UnityEngine;

[CreateAssetMenu(fileName = "ThisIsNotACliche", menuName = "Weapons/ThisIsNotACliche", order = 0)]
public class ThisIsNotACliche : Weapon
{
    protected override void Shoot(Vector3 position, Vector3 direction, float damage)
    {
        GameObject bullet_obj = GameObject.Instantiate(bullet, position, Quaternion.identity);
        bullet_obj.GetComponent<BulletClass>().Criator(damage, range, direction, bullet_speed);
    }
}