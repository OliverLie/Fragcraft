using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    [Header("Experience")]
    [SerializeField] AnimationCurve experienceCurve;
    public int currentLevel = 0;
    public int totalExperience = 0;
    private int previousLevelsExperience = 0;
    private int nextLevelsExperience = 0;
  

    [Header("UI")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Image experienceFill;

    void Start()
    {

        UpdateLevelUI();
    }

    public void AddExperience(int amount)
    {
        totalExperience += amount;
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

            Debug.Log($"Level up! Now level {currentLevel}");

            // Trigger WCSMenu level-up
            WCSMenu menu = FindFirstObjectByType<WCSMenu>();
            if (menu != null)
            {
                menu.ShowLevelUpMenu();
            }
        }
    }

    private void UpdateLevelUI()
    {
        int start = totalExperience - previousLevelsExperience;
        int end = Mathf.Max(1, nextLevelsExperience - previousLevelsExperience); // undgå div/0
        levelText.text = currentLevel.ToString();
        experienceText.text = $"{start} exp / {end} exp";
        experienceFill.fillAmount = Mathf.Clamp01((float)start / (float)end);
    }
}
