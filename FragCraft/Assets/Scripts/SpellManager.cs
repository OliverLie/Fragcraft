using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public WCSClass playerClass;
    public int[] spellLevels;

    public void Start()
    {

    
    if (playerClass != null && (spellLevels == null || spellLevels.Length != playerClass.spells.Length))
    {
        spellLevels = new int[playerClass.spells.Length];
        // Sæt alle til level 1 for test
        for (int i = 0; i < spellLevels.Length; i++) spellLevels[i] = 1;
    }
    

    }
    public void UseSpell(int index)
    {
        if (index < 0 || index >= playerClass.spells.Length) return;

        Spell spell = playerClass.spells[index];
        int level = spellLevels[index];

        if (level <= 0) return; // spell ikke unlocked

        // Udfør spell effekt
        switch (spell.type)
        {
            case SpellType.VampiricAura:
                ApplyVampiricAura(level);
                break;
            case SpellType.Levitation:
                ApplyLevitation(level);
                break;
            case SpellType.UnholyAura:
                ApplyUnholyAura(level);
                break;
            case SpellType.SuicideBomber:
                ApplySuicideBomber(level);
                break;
            case SpellType.Fireball:
                CastFireball(level);
                break;
            case SpellType.Heal:
                CastHeal(level);
                break;
            case SpellType.Blink:
                CastBlink(level);
                break;
        }
    }

    private void ApplyVampiricAura(int level)
    {
        Debug.Log($"Vampiric Aura aktiveret, level {level} (lifesteal)");
        // Her kan du f.eks. give spilleren +X% lifesteal
    }

    private void ApplyLevitation(int level)
    {
        Debug.Log($"Levitation aktiveret, level {level} (hop højere/fald langsomt)");
        // Ændre player's jump force eller gravity scale
    }

    private void ApplyUnholyAura(int level)
    {
        Debug.Log($"Unholy Aura aktiveret, level {level} (bonus speed)");
        // F.eks. øget movement speed
    }

    private void ApplySuicideBomber(int level)
    {
        Debug.Log($"Suicide Bomber aktiveret, level {level} (spræng skade)");
        // Spawn explosion på spillerens position
    }

    private void CastFireball(int level)
    {
        Debug.Log($"Cast Fireball, level {level}");
        // Instantiate et prefab med projectile + damage
    }

    private void CastHeal(int level)
    {
        Debug.Log($"Cast Heal, level {level}");
        // Tilføj liv til player
    }

    private void CastBlink(int level)
    {
        Debug.Log($"Cast Blink, level {level}");
        // Teleport player en kort distance
    }
}
