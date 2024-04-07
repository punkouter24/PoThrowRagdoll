using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
    private Vector3 startPosition;
    private bool hasScored = false;
    private float movementThreshold = 1.5f; // Adjust based on gameplay needs

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (!hasScored && Vector3.Distance(startPosition, transform.position) > movementThreshold)
        {
            // This block has been moved sufficiently; increase the score
            ScoreController.instance.AddScore(1);
            hasScored = true;
        }
    }
}
