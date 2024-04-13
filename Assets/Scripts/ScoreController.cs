using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    public Text scoreText;
    private int score = 0;

    // Method to get the current score
    public int GetScore()
    {
        return score;
    }

    void Awake()
    {
        // Ensure there's only one instance of ScoreManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent
            score = 0; // Explicitly set score to 0
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        // Assuming you have a Text UI component to display the score
        scoreText.text = "Score: " + score;
    }

    // Call this method to reset the score, e.g., when restarting the level
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }
}
