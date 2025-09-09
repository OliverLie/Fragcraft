// FireballProjectile.cs
using Unity.Mathematics;
using UnityEngine;
public class FireballProjectile : MonoBehaviour
{
    [SerializeField] GameObject ImpactVFX;
    public MeshRenderer mesh;
    public GameObject effect1;
    public GameObject effect2;
    public Health health;
    public float damage = 80f;
    
    
void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Enemy"))
    {
        Health enemyHealth = collision.gameObject.GetComponentInParent<Health>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage, transform.forward);
            Debug.Log("Enemy tog skade, HP nu: " + enemyHealth.currentHealth);
        }
        else
        {
            Debug.LogWarning("Ingen Health fundet på: " + collision.gameObject.name);
        }
    }

    // resten af effekterne
    mesh.enabled = false;
    Destroy(effect1, 0);
    Destroy(effect2, 0);

    GameObject impactFireballVFX = Instantiate(ImpactVFX, transform.position, transform.rotation);
    Destroy(impactFireballVFX, 1f);
}



}
