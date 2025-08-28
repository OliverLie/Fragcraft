using UnityEngine;

[CreateAssetMenu(menuName = "WCS/Class")]
public class WCSClass : ScriptableObject
{
    public string className;
    public string description;
    public Sprite classIcon;

    public Spell[] spells; // 4 spells typisk
}
