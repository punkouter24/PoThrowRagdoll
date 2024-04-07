using UnityEngine;
using System.Collections; // Needed for IEnumerator

public class RagdollThrower : MonoBehaviour
{
    public Rigidbody ragdollRoot; // Assign the root Rigidbody of the ragdoll in the Inspector
    public float throwForce = 500f;
    public LevelController levelController; // Make sure to assign this in the Inspector
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool hasThrown = false;
    private int score = 0; // Tracks the score

    //void Start()
    //{
    //    // Store the initial position and rotation
    //    initialPosition = ragdollRoot.transform.position;
    //    initialRotation = ragdollRoot.transform.rotation;

    //    SetRagdollState(true); // Disable physics initially to keep the ragdoll in T-pose
    //}

    public void SetRagdollState(bool isKinematic)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = isKinematic;
        }
    }

    // Call this method when the THROW button is pressed
    public void OnThrowButtonPressed()
    {
        if (!hasThrown) // Check to make sure we only throw once
        {
            gameObject.SetActive(true); // Ensure the game object is active
            SetRagdollState(false); // Enable ragdoll physics before throwing
            Vector3 throwDirection = new Vector3(0, 0, 1); // Example direction
            ThrowRagdoll(throwDirection);
            StartCoroutine(LevelEndCountdown(10f)); // Start the 5-second countdown after throwing
            hasThrown = true;
        }
    }

    // Coroutine to delay the end of the level by 5 seconds after the ragdoll is thrown
    private IEnumerator LevelEndCountdown(float delay)
    {
        yield return new WaitForSeconds(delay);
        levelController.LevelComplete(); // Notify the LevelManager to proceed to the next level
    }

    public void ThrowRagdoll(Vector3 direction)
    {
        ragdollRoot.AddForce(direction * throwForce, ForceMode.Impulse);
    }

    public void ResetRagdoll()
    {
        // Reset position and rotation to initial values
        ragdollRoot.transform.position = initialPosition;
        ragdollRoot.transform.rotation = initialRotation;

        // Reset physics state
        ragdollRoot.linearVelocity = Vector3.zero; // Stop any movement
        ragdollRoot.angularVelocity = Vector3.zero; // Stop any rotational movement
        SetRagdollState(true); // Return to kinematic state to freeze in place

        // Optionally, reset the state of child Rigidbody components if your ragdoll has them
        foreach (var rb in ragdollRoot.GetComponentsInChildren<Rigidbody>())
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        hasThrown = false; // Allow the ragdoll to be thrown again
    }

    // Method to update throw force from a UI slider
    public void UpdateThrowForce(float value)
    {
        throwForce = value;
        Debug.Log($"Throw force updated to: {throwForce}");
    }

}
