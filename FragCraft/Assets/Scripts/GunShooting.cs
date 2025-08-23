using UnityEditor;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactforce = 30f;
    public float fireRate = 3f;
    public int AKMagSize = 30;
    public int AKMagazines = 3;
    public float NextTimeToFire = 0f;

    public Camera cam;
    public ParticleSystem Muzzle;
    public GameObject ImpactVFX;


    void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
        {
            NextTimeToFire = Time.time + 1f / fireRate;
            Shoot();

        }
        AKReload();

    }

    void Shoot()
    {
        if (AKMagSize > 0)
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
                //Pushed Objects with a Rigidbody
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactforce);

                }
                //Adds a particle effect at the end of a ray, and instantiate it as a GO. 
                GameObject ImpactGO = Instantiate(ImpactVFX, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(ImpactGO, 2f);
                AKMagSize -= 1;
                Debug.Log(AKMagSize);


            }
        }

    }
    

    void AKReload() //Add a Reload animation in the future
    {
        if (Input.GetButtonDown("Reload") && AKMagazines > 0)
        {
            Debug.Log("Reloading");
            AKMagSize = 30;
            //Sets the magazine to one less everytime you reload
            AKMagazines -= 1;

        }

    }
}
