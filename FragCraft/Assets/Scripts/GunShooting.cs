using UnityEngine;
using TMPro;



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
    public TextMeshProUGUI AmmoUI;
    public TextMeshProUGUI MagazineUI;

    void Ready()
    {
        AmmoUI = GetComponent<TextMeshProUGUI>();
        MagazineUI = GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
        {
            NextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        AKReload();
        MagazineUI.SetText("Mags: {0} / 3", AKMagazines);
        AmmoUI.SetText("{0} / 30", AKMagSize);
    }

    void Shoot()
    {
        if (AKMagSize > 0)
        {
            Muzzle.Play();
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                // Hitbox damage
                var hitBox = hit.collider.GetComponent<Hitbox>();
                if (hitBox)
                {
                    hitBox.OnRaycastHit(this, cam.transform.forward); // ✅ direction sat korrekt
                }

                // Skub objekter med Rigidbody
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactforce);
                }

                // Impact effekt
                GameObject ImpactGO = Instantiate(ImpactVFX, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(ImpactGO, 2f);

                AKMagSize -= 1;
                Debug.Log("Ammo left: " + AKMagSize);
            }
        }
    }

    void AKReload()
    {
        if (Input.GetButtonDown("Reload") && AKMagazines > 0 && AKMagSize != 30)
        {
            
            Debug.Log("Reloading");
            AKMagSize = 30;
            AKMagazines -= 1;
        }
    }
}
