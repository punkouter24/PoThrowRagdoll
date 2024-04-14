using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
    private Vector3 startPosition;
    private bool hasScored = false;
    private float movementThreshold = 1.5f; // Adjust based on gameplay needs
    private ScoreController scoreController; // Reference to the ScoreController

    void Start()
    {
        startPosition = transform.position;
        scoreController = FindObjectOfType<ScoreController>(); // Find the ScoreController in the scene
    }

    void Update()
    {
        if (!hasScored && Vector3.Distance(startPosition, transform.position) > movementThreshold)
        {
            // This block has been moved sufficiently; increase the score
            hasScored = true;
            scoreController.AddScore(1); // Add 1 point to the score
        }
    }
}
