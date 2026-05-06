using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the in-game pause menu.
/// Freezes and resumes gameplay by manipulating Time.timeScale.
/// The pause button is hidden while the pause menu is open to prevent overlap.
/// </summary>
public class menuPausa : MonoBehaviour
{
    public GameObject PauseMenu;   // The pause menu panel (shown when paused, hidden during play)
    public GameObject pauseButton; // The button used to open the pause menu (hidden while paused)

    /// <summary>
    /// Freezes the game and shows the pause menu.
    /// Called by the pause button in the HUD.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0f;          // Freeze all time-based logic (physics, animations, coroutines)
        pauseButton.SetActive(false); // Hide the pause button so it doesn't overlap the menu
        PauseMenu.SetActive(true);    // Show the pause panel
    }

    /// <summary>
    /// Resumes normal gameplay and hides the pause menu.
    /// Called by the "Resume" button inside the pause menu.
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1f;         // Restore normal time scale
        pauseButton.SetActive(true); // Bring back the pause button in the HUD
        PauseMenu.SetActive(false);  // Hide the pause panel
    }

    /// <summary>
    /// Reloads the current scene, resetting all game state.
    /// Time.timeScale is restored to 1 before loading to prevent the new scene starting frozen.
    /// Called by the "Restart" button inside the pause menu.
    /// </summary>
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // Must be reset here because LoadScene does not reset timeScale automatically
    }

    /// <summary>
    /// Exits the application.
    /// Debug.Log is included for testing in the Editor, where Application.Quit() has no effect.
    /// Called by the "Quit" button inside the pause menu.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Closing game");
        Application.Quit();
    }
}
