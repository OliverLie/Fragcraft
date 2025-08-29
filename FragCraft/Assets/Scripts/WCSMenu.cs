using UnityEngine;

public class WCSMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private CanvasGroup menuCanvasGroup;
    [SerializeField] private CanvasGroup ClassCanvasGroup;
    [SerializeField] private CanvasGroup BackGroundFiller;
    [SerializeField] private SpellManager spellManager;

    private bool menuOpen = false;
    private bool ClassMenuOpen = false;

    private void Awake()
    {
        if (menuCanvasGroup != null)
            CloseMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            OpenMenu();

        }

        if (menuOpen || ClassMenuOpen)
        {
            // Tast 1 = første spell, tast 2 = anden spell, osv.
            if (Input.GetKeyDown(KeyCode.Alpha1))
                TryUpgradeSpell(0);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                TryUpgradeSpell(1);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                TryUpgradeSpell(2);

            if (Input.GetKeyDown(KeyCode.Alpha4))
                TryUpgradeSpell(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            CloseMenu();
        }
    }

    public void OpenMenu()
    {
        if (menuCanvasGroup != null)
        {
            menuCanvasGroup.alpha = 1f;
            menuCanvasGroup.interactable = true;
            menuCanvasGroup.blocksRaycasts = true;
            menuOpen = true;
            BackGroundFiller.alpha = 1f;
        }
    }

    public void OpenClassMenu()
    {
        if (ClassCanvasGroup != null)
        {
            ClassCanvasGroup.alpha = 1f;
            ClassCanvasGroup.interactable = true;
            ClassCanvasGroup.blocksRaycasts = true;
            ClassMenuOpen = true;
            BackGroundFiller.alpha = 1f;
        }
    }


    public void CloseMenu()
    {
        if (menuCanvasGroup != null)
        {
            menuCanvasGroup.alpha = 0f;
            menuCanvasGroup.interactable = false;
            menuCanvasGroup.blocksRaycasts = false;
            menuOpen = false;
            BackGroundFiller.alpha = 0f;

        }

        if (ClassCanvasGroup != null)
        {
            ClassCanvasGroup.alpha = 0f;
            ClassCanvasGroup.interactable = false;
            ClassCanvasGroup.blocksRaycasts = false;
            ClassMenuOpen = false;
        }
    }

    private void TryUpgradeSpell(int index)
    {
        ExperienceManager xp = FindFirstObjectByType<ExperienceManager>();
        if (xp != null && xp.SpendPoint())
        {
            spellManager.UpgradeSpell(index);
            CloseMenu();
        }
        else
        {
            Debug.Log("Ingen upgrade points!");
        }
    }
}
