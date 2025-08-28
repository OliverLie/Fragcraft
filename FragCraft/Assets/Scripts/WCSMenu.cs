using UnityEngine;

public class WCSMenu : MonoBehaviour
{
    [Header("Class Info")]
    public WCSClass currentClass;
    public int[] spellLevels;

    [Header("UI Panels (use CanvasGroup)")]
    public CanvasGroup MenuPanelGroup;
    public CanvasGroup ClassPanelGroup;
    public CanvasGroup BackgroundGroup;
    public SpellManager spellManager;

    private bool menuOpen = false;
    private bool canUpgrade = false;

    void Update()
    {
        SpellUse();
        HandleUpgradeInput();

        // Manual open menu
        if (Input.GetKeyDown(KeyCode.M))
        {
            SetCanvasGroup(MenuPanelGroup, true);
            SetCanvasGroup(BackgroundGroup, true);
            menuOpen = true;
            canUpgrade = false; // her kun til browsing, ikke opgradering
        }

        // Close menu
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetCanvasGroup(MenuPanelGroup, false);
            SetCanvasGroup(BackgroundGroup, false);
            CloseMenu();
        }
    }

    #region Class Handling

    public void SelectClass(WCSClass newClass)
    {
        currentClass = newClass;
        spellLevels = new int[currentClass.spells.Length];
        Debug.Log($"Selected class: {currentClass.className}");
    }

    public void ShowLevelUpMenu()
    {
        if (currentClass == null)
        {
            Debug.LogWarning("No class selected!");
            return;
        }

        SetCanvasGroup(ClassPanelGroup, true);
        SetCanvasGroup(BackgroundGroup, true);

        menuOpen = true;
        canUpgrade = true;

        Debug.Log("Level up! Vælg en spell at opgradere (1-4).");
    }

    private void HandleUpgradeInput()
    {
        if (!menuOpen || !canUpgrade || currentClass == null) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) { UpgradeSpell(0); }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { UpgradeSpell(1); }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { UpgradeSpell(2); }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) { UpgradeSpell(3); }
    }

    private void UpgradeSpell(int index)
    {
        if (index < 0 || index >= spellLevels.Length) return;

        if (spellLevels[index] < currentClass.spells[index].maxLevel)
        {
            spellLevels[index]++;
            Debug.Log($"Upgraded {currentClass.spells[index].spellName} to level {spellLevels[index]}");
        }
        else
        {
            Debug.Log($"{currentClass.spells[index].spellName} is already max level!");
        }

        canUpgrade = false;
        CloseMenu();
    }

    private void CloseMenu()
    {
        SetCanvasGroup(ClassPanelGroup, false);
        SetCanvasGroup(BackgroundGroup, false);

        menuOpen = false;
        canUpgrade = false;
    }

    private void SetCanvasGroup(CanvasGroup group, bool visible)
    {
        group.alpha = visible ? 1f : 0f;
        group.interactable = visible;
        group.blocksRaycasts = visible;
    }

    #endregion

    public void SpellUse()
    {

    if (Input.GetKeyDown(KeyCode.U)) spellManager.UseSpell(0);
    if (Input.GetKeyDown(KeyCode.I)) spellManager.UseSpell(1);
    if (Input.GetKeyDown(KeyCode.O)) spellManager.UseSpell(2);
    if (Input.GetKeyDown(KeyCode.P)) spellManager.UseSpell(3);
    }

    
}
