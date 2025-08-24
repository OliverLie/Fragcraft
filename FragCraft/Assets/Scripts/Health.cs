using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    [HideInInspector] public float currentHealth;

    SkinnedMeshRenderer[] skinnedMeshRenderers;
    UiHealthbar healthbar;
    public float blinkIntensity = 5f;
    public float blinkDuration = 0.2f;
    float blinkTimer;

    Ragdoll ragdoll;
    PotentialAIScript ai;

    void Start()
    {
        // Mesh og UI
        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        healthbar = GetComponentInChildren<UiHealthbar>();
        ragdoll = GetComponent<Ragdoll>();
        ai = GetComponent<PotentialAIScript>();

        currentHealth = maxHealth;

        // Tilføj hitboxes til alle rigidbodies
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
        if (healthbar != null)
            healthbar.SetHealthBarPercentage(currentHealth / maxHealth);

        if (currentHealth <= 0f)
        {
            Die(direction);

        }

        blinkTimer = blinkDuration;
    }

    void Die(Vector3 direction)
    {
        if (ragdoll != null)
        {
            ragdoll.ActiveRagdoll();
            ragdoll.ApplyForce(direction * 5f); // Spark i dødsretningen
        }

        if (ai != null)
            ai.enabled = false; // Sluk AI’en

        transform.Find("Canvas").gameObject.SetActive(false);


      
    }

    void Update()
    {
        // Blink-effekt
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = lerp * blinkIntensity;

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
