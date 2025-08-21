using UnityEditor;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactforce = 30f;
    public float fireRate = 3f;
    public Camera cam;
    public ParticleSystem Muzzle;
    public GameObject ImpactVFX;
    public float AKMagSize = 30f;

    public float NextTimeToFire = 0f;

    void Update()
    {
        {
            if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
            {
                NextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Muzzle.Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {

            Enemy enemy = hit.transform.GetComponent<Enemy>();

            Debug.Log(hit.transform.name);

            if (enemy != null)
            {
                enemy.TakeDamage(damage);

            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactforce);

            }

            GameObject ImpactGO = Instantiate(ImpactVFX, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ImpactGO, 2f);
            
            
        }

    }
}
