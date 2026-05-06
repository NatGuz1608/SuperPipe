using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the end-of-game state: displays the victory panel, freezes the game,
/// and provides a method to restart the current scene.
/// Both GameManager instances share a reference to this single FinalManager.
/// The first player to reach metaPuntos triggers DeclararGanador().
/// </summary>
public class FinalManager : MonoBehaviour
{
    [Header("Victory UI")]
    public GameObject panelFinal;         // Panel shown when a player wins; hidden during gameplay
    public TextMeshProUGUI textoGanador;  // Text element that displays the winner's name

    void Awake()
    {
        // Ensure the victory panel is hidden at the start of every scene
        if (panelFinal != null) panelFinal.SetActive(false);
        Time.timeScale = 1f; // Guarantee normal time scale in case it was frozen in a previous session
    }

    /// <summary>
    /// Called by GameManager when a player reaches the point goal.
    /// Reveals the victory panel and freezes gameplay by setting timeScale to 0.
    /// </summary>
    /// <param name="nombreDelGanador">The winning player's display name (e.g. "Player 1").</param>
    public void DeclararGanador(string nombreDelGanador)
    {
        panelFinal.SetActive(true); // Show the victory overlay
        textoGanador.text = "!" + nombreDelGanador + " HAS WON!"; // Display winner's name

        // Freeze all time-based logic (physics, coroutines, animations) to pause the game
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Reloads the current scene, resetting all game state.
    /// Intended to be called by the "Play Again" button in the victory panel.
    /// Note: Time.timeScale is restored automatically when the scene reloads
    /// because Awake() sets it back to 1.
    /// </summary>
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
