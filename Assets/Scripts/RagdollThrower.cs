using System.Collections; // Needed for IEnumerator
using UnityEngine;
using UnityEngine.UI; // Required for accessing the Slider component

public class RagdollThrower : MonoBehaviour
{
    public Rigidbody ragdollRoot; // Assign the root Rigidbody of the ragdoll in the Inspector
    public float throwForce = 500f;
    public float throwAngle = 45f; // Default angle in degrees
    public Slider angleSlider; // Assign this in the Inspector
    public LevelController levelController; // Make sure to assign this in the Inspector
    public GameObject ThrowSquare; // Add this line to declare ThrowSquare
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool hasThrown = false;

    private void Start()
    {
        SetRagdollState(true); // Disable physics initially to keep the ragdoll in T-pose
    }

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
            //ragdollRoot.transform.position += Vector3.up; // Raise the ragdoll by 1 unit
            gameObject.SetActive(true); // Ensure the game object is active
            SetRagdollState(false); // Enable ragdoll physics before throwing
            float angleRad = throwAngle * Mathf.Deg2Rad; // Convert angle to radians
                                                         // Retrieve the x position of the ThrowSquare and calculate direction offset
            float xDirection = ThrowSquare.transform.position.x;
            Debug.Log(xDirection);
            xDirection = Mathf.Clamp(((xDirection - 0) / (500 - 0) * (1 - (-1))) + (-1), -0.1f, 0.1f);
            Debug.Log($"Clamped xDirection: {xDirection}");

            Vector3 throwDirection = new(xDirection, Mathf.Cos(angleRad), Mathf.Sin(angleRad)); // Adjust direction based on angle and x position
            ThrowRagdoll(throwDirection);
            _ = StartCoroutine(LevelEndCountdown(10f)); // Start the 5-second countdown after throwing
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
        foreach (Rigidbody rb in ragdollRoot.GetComponentsInChildren<Rigidbody>())
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

    public void UpdateThrowAngle(float value)
    {
        throwAngle = value;
        Debug.Log($"Throw angle updated to: {throwAngle} degrees");
    }
}
