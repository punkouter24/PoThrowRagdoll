using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public AudioClip buttonClickSound; // The click sound effect
    public AudioSource audioSource;    // Reference to the AudioSource component
    public Text scoreText;
  
    public void Start()
    {
        if (PlayerPrefs.HasKey("FinalScore"))
        {
            int finalScore = PlayerPrefs.GetInt("FinalScore");
            scoreText.text = "Final Score: " + finalScore;
        }
    }

    public void StartGame()
    {
        PlaySound();
        SceneManager.LoadScene("Game"); // Change "Game" to your game scene's name
    }

    // Function to be called when the "Quit" button is pressed
    public void QuitGame()
    {
        PlaySound();
        // Quit application
#if UNITY_EDITOR
        // If running in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // If running in a build version
            Application.Quit();
#endif
    }

    // Helper function to play the button click sound
    void PlaySound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }
}
