using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Health health;


    public void OnRaycastHit(GunShooting Shoot, Vector3 direction)
    {
        health.TakeDamage(Shoot.damage, direction);

    }
}
