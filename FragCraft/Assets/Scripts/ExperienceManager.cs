using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    [Header("Experience Settings")]
    [SerializeField] AnimationCurve experienceCurve;
    public int currentLevel = 0;
    public int totalExperience = 0;

    private int previousLevelsExperience = 0;
    private int nextLevelsExperience = 0;

    [Header("Level-Up Points")]
    public int availablePoints = 0;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Image experienceFill;

    void Start()
    {
        previousLevelsExperience = (int)experienceCurve.Evaluate(currentLevel);
        nextLevelsExperience = (int)experienceCurve.Evaluate(currentLevel + 1);

        UpdateLevelUI();
    }

    public void AddExperience(int amount)
    {
        totalExperience += amount;
        Debug.Log($"Gained {amount} XP (Total: {totalExperience})");

        CheckForLevelUp();
        UpdateLevelUI();
    }

    private void CheckForLevelUp()
    {
        while (totalExperience >= nextLevelsExperience)
        {
            currentLevel++;
            previousLevelsExperience = (int)experienceCurve.Evaluate(currentLevel);
            nextLevelsExperience = (int)experienceCurve.Evaluate(currentLevel + 1);

            availablePoints++;
            Debug.Log($"Level up! Now level {currentLevel}. Upgrade points: {availablePoints}");

            // Åbn menu med CanvasGroup
            WCSMenu menu = FindFirstObjectByType<WCSMenu>();
            if (menu != null)
            {
                menu.OpenClassMenu();
            }
        }
    }

    private void UpdateLevelUI()
    {
        int start = totalExperience - previousLevelsExperience;
        int end = Mathf.Max(1, nextLevelsExperience - previousLevelsExperience);

        if (levelText != null) levelText.text = $"{currentLevel}";
        if (experienceText != null) experienceText.text = $"{start} / {end} XP";
        if (experienceFill != null) experienceFill.fillAmount = Mathf.Clamp01((float)start / (float)end);
    }

    public bool SpendPoint()
    {
        if (availablePoints > 0)
        {
            availablePoints--;
            return true;
        }
        else
        {
            Debug.Log("Ingen points tilgængelige til opgradering!");
            return false;
        }
    }
}
