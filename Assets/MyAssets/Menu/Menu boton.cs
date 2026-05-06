using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles button interactions on the main menu.
/// Plays a click sound before performing any scene transition to avoid
/// an abrupt cut-off when the scene unloads the AudioSource.
/// </summary>
public class Menuboton : MonoBehaviour
{
    public AudioSource fuenteSonido; // AudioSource used to play UI sound effects
    public AudioClip sonidoClick;    // Sound clip played when any button is pressed

    /// <summary>
    /// Called by the "Play" button.
    /// Plays the click sound first, then loads the next scene after a short delay
    /// so the sound has time to finish before the scene transitions.
    /// </summary>
    public void Jugar()
    {
        // 1. Sound plays first
        fuenteSonido.PlayOneShot(sonidoClick);

        // 2. Then we wait 0.2 seconds before changing the scene
        Invoke("CargarSiguienteEscena", 0.2f);
    }

    /// <summary>
    /// Loads the next scene in the build order (main menu = index 0, game = index 1, etc.)
    /// Separated from Jugar() so Invoke() can reference it by name.
    /// </summary>
    void CargarSiguienteEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Called by the "Quit" button.
    /// Plays the click sound and exits the application.
    /// Debug.Log is included for testing in the Editor, where Application.Quit() has no effect.
    /// </summary>
    public void Salir()
    {
        fuenteSonido.PlayOneShot(sonidoClick);
        Debug.Log("Closing game");
        Application.Quit();
    }
}
