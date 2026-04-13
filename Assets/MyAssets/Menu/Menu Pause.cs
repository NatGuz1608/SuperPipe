// ============================================================
// Menu_Pause.cs
// Handles pause menu visibility and game state toggling.
// Allows the player to pause, resume, restart, or quit the game.
// ============================================================

using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPausa : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject pauseButton;

    /// <summary>
    /// Freezes game time and displays the pause menu UI.
    /// Hides the pause button to prevent double-triggering.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0f;          // Freeze all time-dependent logic
        pauseButton.SetActive(false); // Hide the pause trigger button
        PauseMenu.SetActive(true);    // Show the pause menu panel
    }

    /// <summary>
    /// Restores game time and hides the pause menu UI.
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1f;          // Restore normal time scale
        pauseButton.SetActive(true);  // Re-enable the pause trigger button
        PauseMenu.SetActive(false);   // Hide the pause menu panel
    }

    /// <summary>
    /// Reloads the current active scene, effectively restarting the game.
    /// Ensures time scale is reset before reloading to avoid a frozen scene.
    /// </summary>
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // Safety reset in case game was paused before restart
    }

    /// <summary>
    /// Exits the application. In the editor, logs a message instead of quitting.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("cerrando juego");
        Application.Quit();
    }
}