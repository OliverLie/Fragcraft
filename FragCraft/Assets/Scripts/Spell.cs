using System;
using UnityEngine;

[CreateAssetMenu(menuName = "WCS/Spell")]
public class Spell : ScriptableObject
{
    public string spellName;
    public string description;
    public Sprite icon;
    public int maxLevel = 4;

    // Effekter kan styres gennem events, enums, eller custom logik
    // For nu bare en "type" der kan bruges i manager
    public SpellType type;


}

public enum SpellType
{
    VampiricAura,
    Levitation,
    UnholyAura,
    SuicideBomber,
    Fireball,
    AirBender,
    Fly,
    Heal,
    Blink
    // ... tilføj flere
}
