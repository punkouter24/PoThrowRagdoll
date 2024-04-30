using UnityEngine;
using UnityEngine.UI;

public class LevelDisplayController : MonoBehaviour
{
    public Text levelText;
    private LevelController levelController;

    [System.Obsolete]
    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        if (levelController != null)
        {
            UpdateLevelText();
        }
    }

    void Update()
    {
        if (levelController != null)
        {
            UpdateLevelText();
        }
    }

    void UpdateLevelText()
    {
        levelText.text = "Level: " + levelController.currentLevelNumber;
    }
}