using UnityEngine;

[System.Serializable]
public class WcsSpell
{
    public string spellName;
    public int maxLevel = 5;
}

[CreateAssetMenu(fileName = "NewClass", menuName = "WCS/Class")]
public class WCSClass : ScriptableObject
{
    public string className;
    public Spell[] spells;
}
