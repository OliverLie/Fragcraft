using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public WCSClass playerClass;
    public int[] spellLevels;
    public float Cooldown = 10f;
    public float SpellTravelSpeed = 20f;
    public Camera cam;
    public float spawnDistance = 1f;
    [SerializeField] Transform PlayerCast;
    [SerializeField] GameObject FireBallVFX;


    void Start()

    {
        
        
        if (playerClass != null)
        {
            spellLevels = new int[playerClass.spells.Length];
        }
    }

    void Update()
    {
    if (Input.GetKeyDown(KeyCode.U)) UseSpell(0);
    if (Input.GetKeyDown(KeyCode.I)) UseSpell(1);
    if (Input.GetKeyDown(KeyCode.O)) UseSpell(2);
    if (Input.GetKeyDown(KeyCode.P)) UseSpell(3);
    }

    public void SelectClass(WCSClass newClass)
    {
        playerClass = newClass;
        spellLevels = new int[playerClass.spells.Length];
        Debug.Log($"Class valgt: {playerClass.className}");
    }

    public void UpgradeSpell(int index)
    {
        if (playerClass == null)
        {
            Debug.LogWarning("Ingen class valgt!");
            return;
        }

        if (index < 0 || index >= spellLevels.Length)
        {
            Debug.LogWarning("Spell index udenfor rækkevidde!");
            return;
        }

        if (spellLevels[index] < playerClass.spells[index].maxLevel)
        {
            spellLevels[index]++;
            Debug.Log($"Upgraderede {playerClass.spells[index].spellName} til level {spellLevels[index]}");
        }
        else
        {
            Debug.Log($"{playerClass.spells[index].spellName} er allerede max level!");
        }
    }

    public void UseSpell(int index)
    {
        if (index < 0 || index >= playerClass.spells.Length) return;

        Spell spell = playerClass.spells[index];
        int level = spellLevels[index];

        if (level <= 0) return; // spell ikke unlocked

        switch (spell.type)
        {
            case SpellType.VampiricAura:
                //ApplyVampiricAura(level);
                break;
            case SpellType.Levitation:
                //ApplyLevitation(level);
                break;
            case SpellType.UnholyAura:
                //ApplyUnholyAura(level);
                break;
            case SpellType.SuicideBomber:
                //ApplySuicideBomber(level);
                break;
            case SpellType.Fireball:
                CastFireball(level);
                break;
            case SpellType.AirBender:
                CastAirbender(level);
                break;
            case SpellType.Fly:
                CastFly(level);
                break;
        }
        
    }

    private void CastFly(int level)
    {
        throw new NotImplementedException();
    }

    private void CastAirbender(int level)
    {
        throw new NotImplementedException();
    }

    private void CastFireball(int level)
    {
        // Retning fra kamera
        Vector3 shootDirection = Camera.main.transform.forward;

        // Spawn position lidt foran kamera
        Vector3 spawnPosition = Camera.main.transform.position + shootDirection * spawnDistance;

        // Rotation i retning af shootDirection
        Quaternion spawnRotation = Quaternion.LookRotation(shootDirection);

        // Instantiate projectile
        GameObject projectile = Instantiate(FireBallVFX, spawnPosition, spawnRotation);

        // Hent Rigidbody og giv hastighed
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * SpellTravelSpeed;
            rb.useGravity = false; // Valgfrit, hvis du ikke vil have gravity
        }
    }
}
    



    




    


    

