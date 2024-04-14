using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    // Method to get the current score
    public int GetScore()
    {
        return score;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    // Call this method to reset the score, e.g., when restarting the level
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }
}
