// ============================================================
// Menu_boton.cs
// Controls main menu button interactions.
// Plays a click sound effect before executing scene transitions
// to ensure audio feedback is heard before the scene unloads.
// ============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuboton : MonoBehaviour
{
    public AudioSource fuenteSonido; // AudioSource component used to play UI sounds
    public AudioClip sonidoClick;    // Click sound clip assigned via the Inspector

    /// <summary>
    /// Plays the click sound and schedules a scene load after a short delay.
    /// The delay ensures the audio clip has time to play before the scene transitions.
    /// </summary>
    public void Jugar()
    {
        fuenteSonido.PlayOneShot(sonidoClick);
        
        // Delayed call to allow audio playback
        Invoke("CargarSiguienteEscena", 0.2f); 
    }

    /// <summary>
    /// Loads the next scene in the Build Settings index order.
    /// Called via Invoke after a brief audio delay.
    /// </summary>
    void CargarSiguienteEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Plays the click sound and exits the application.
    /// In the editor, the quit call has no effect; a log message is shown instead.
    /// </summary>
    public void Salir()
    {
        fuenteSonido.PlayOneShot(sonidoClick);
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}