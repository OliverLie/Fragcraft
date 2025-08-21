using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject DeathVFX;
    public float Health = 50f;



    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
        {

            Death();
        }
    }
  
    void Death()
    {
        Instantiate(DeathVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
