// ============================================================
// GameManager.cs
// Central controller for a single player's side of the game.
// Manages the countdown sequence, enemy spawning, score tracking,
// and audio feedback. Designed to work in tandem with Villano.cs,
// which calls SumarPunto() when an enemy is defeated.
// Each player side has its own independent GameManager instance.
// ============================================================

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI - Referencias Locales")]
    public TextMeshProUGUI textoCuentaAtras; // Countdown text element shown before the game starts
    public TextMeshProUGUI textoPuntos;      // Score display text for this player's side

    [Header("Configuración")]
    public string mensajeFinalCuenta = "¡GOYA!"; // Message shown at the end of the countdown
    public List<GameObject> villanos;            // Pool of enemy GameObjects managed by this instance
    public float intervaloAparicion = 1f;        // Seconds between each enemy spawn attempt

    public AudioSource audioSource;  // AudioSource used for score feedback sound
    public AudioClip clipContador;   // Sound clip played each time a point is scored

    private int puntos = 0;           // Running score for this player's side
    private bool juegoIniciado = false; // Gate flag; prevents spawning before countdown completes

    /// <summary>
    /// Initializes all enemies as inactive and starts the pre-game countdown coroutine.
    /// </summary>
    void Start()
    {
        foreach (var v in villanos) v.SetActive(false); // Ensure all enemies start hidden
        StartCoroutine(CuentaAtras());
    }

    /// <summary>
    /// Coroutine that runs a 3-second numeric countdown followed by a start message.
    /// Once complete, sets the game-started flag and begins the repeating enemy spawn cycle.
    /// </summary>
    IEnumerator CuentaAtras()
    {
        textoCuentaAtras.gameObject.SetActive(true);

        // Count down from 3 to 1, updating the UI text each second
        for (int i = 3; i > 0; i--)
        {
            textoCuentaAtras.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        // Display the final start message briefly before hiding the countdown UI
        textoCuentaAtras.text = mensajeFinalCuenta;
        yield return new WaitForSeconds(0.5f);
        textoCuentaAtras.gameObject.SetActive(false);

        // Unlock spawning and begin the repeating spawn interval
        juegoIniciado = true;
        InvokeRepeating("AparecerVillano", 0.5f, intervaloAparicion);
    }

    /// <summary>
    /// Increments the player's score by one, updates the score UI text,
    /// and plays the scoring sound effect. Called by Villano.cs on enemy death.
    /// </summary>
    public void SumarPunto()
    {
        puntos++;
        if (textoPuntos != null)
        {
            textoPuntos.text = puntos.ToString();
            audioSource.PlayOneShot(clipContador); // Non-interrupting one-shot audio feedback
        }
    }

    /// <summary>
    /// Selects and activates a random inactive enemy from the pool.
    /// Skips spawning if the game has not started or if an enemy is already active.
    /// This enforces a single-enemy-at-a-time rule per side.
    /// Called repeatedly via InvokeRepeating at the configured interval.
    /// </summary>
    void AparecerVillano()
    {
        if (!juegoIniciado || ContarActivos() > 0) return; // Guard: only spawn when field is clear

        // Build a list of eligible (inactive) enemies from the pool
        List<GameObject> candidatos = new List<GameObject>();
        foreach (GameObject v in villanos)
        {
            if (v != null && !v.activeSelf) candidatos.Add(v);
        }

        // Activate a randomly selected candidate from the eligible pool
        if (candidatos.Count > 0)
        {
            int indiceAleatorio = Random.Range(0, candidatos.Count);
            candidatos[indiceAleatorio].SetActive(true);
        }
    }

    /// <summary>
    /// Returns the number of enemies currently active in the hierarchy.
    /// Used to enforce the single-active-enemy rule before spawning a new one.
    /// </summary>
    int ContarActivos()
    {
        int conteo = 0;
        foreach (GameObject v in villanos)
        {
            if (v != null && v.activeInHierarchy) conteo++;
        }
        return conteo;
    }
}