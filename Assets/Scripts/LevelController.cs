using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Added to use SceneManager

public class LevelController : MonoBehaviour
{
    // Assuming you have a reference to RagdollThrower to reset the ragdoll
    public RagdollThrower ragdollThrower;

    public WallLevel wallLevel; // Assign the WallLevel component in the Inspector
    public PyramidLevel pyramidLevel; // Assign the PyramidLevel component in the Inspector
    public CastleLevel castleLevel; // Assign the CastleLevel component in the Inspector
    public int currentLevelNumber = 1;
    private Level currentLevel;

    void Start()
    {
        StartLevel(currentLevelNumber); // Start at level 1
    }

    public void StartLevel(int levelNumber)
    {
        // Ensure the ragdoll is reset at the beginning of each level
        if (ragdollThrower != null)
        {
            ragdollThrower.ResetRagdoll();
        }

        if (currentLevel != null)
        {
            currentLevel.DestroyLevel();
        }

        // Activate the appropriate level game objects and set the currentLevel
        switch (levelNumber)
        {
            case 1:
                wallLevel.gameObject.SetActive(true);
                currentLevel = wallLevel;
                break;
            case 2:
                pyramidLevel.gameObject.SetActive(true);
                currentLevel = pyramidLevel;
                break;
            case 3:
                castleLevel.gameObject.SetActive(true);
                currentLevel = castleLevel;
                break;
            default:
                Debug.LogError("Invalid level number");
                return;
        }

        currentLevel.CreateLevel();
    }

    // Call this method to end the level and transition to the next one
    public void LevelComplete()
    {
        // Invoke any end-of-level logic here, like score calculation or cleanup
        OnLevelEnd();

        // Check if the current level is the last level
        if (currentLevelNumber < 3)
        {
            currentLevelNumber++;
            StartLevel(currentLevelNumber);
        }
        else
        {
            EndGame();
        }
    }

    private void OnLevelEnd()
    {
        // Any cleanup or reset actions can go here
        // For instance, you might want to show a level completion UI or message
        Debug.Log("Level " + currentLevelNumber + " complete.");

        // If using RagdollThrower to reset ragdoll position and state
        if (ragdollThrower != null)
        {
            ragdollThrower.ResetRagdoll();
        }
    }

    private void EndGame()
    {
        // Handle the end of the game
        Debug.Log("Game Over! Returning to the main menu.");
        SceneManager.LoadScene("Main");
    }
}
