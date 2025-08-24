using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;

    SkinnedMeshRenderer[] skinnedMeshRenderers;
    UiHealthbar Healthbar;
    public float blinkIntensity;
    public float blinkDuration;
    public float blinkTimer;
    public float dieForce;

    Ragdoll ragdoll;

    void Start()
    {
        // Hent ALLE skinned mesh renderers på spilleren
        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        Healthbar = GetComponentInChildren<UiHealthbar>();
        ragdoll = GetComponent<Ragdoll>();
        currentHealth = maxHealth;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            Hitbox hitBox = rigidBody.gameObject.AddComponent<Hitbox>();
            hitBox.health = this;
        }
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        Healthbar.SetHealthBarPercentage(currentHealth / maxHealth);
        if (currentHealth <= 0.0f)
        {
            Die(direction);
        }

        blinkTimer = blinkDuration;
    }

    public void Die(Vector3 direction)
    {
        ragdoll.ActiveRagdoll();
        direction.y = 1;
        ragdoll.ApplyForce(direction * dieForce);
        Healthbar.gameObject.SetActive(false);

    }

    void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = lerp * blinkIntensity;

        // Loop gennem ALLE meshes
        foreach (var renderer in skinnedMeshRenderers)
        {
            foreach (var mat in renderer.materials)
            {
                if (mat.HasProperty("_EmissionColor"))
                {
                    mat.EnableKeyword("_EMISSION");
                    Color emissionColor = Color.white * intensity;
                    mat.SetColor("_EmissionColor", emissionColor);
                }
            }
        }
    }
}
