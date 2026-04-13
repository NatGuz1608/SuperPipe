// ============================================================
// Villano.cs
// Controls a single enemy (villano) appearance cycle.
// The enemy rises from a hidden position, becomes vulnerable
// for a set duration, and retreats if not defeated in time.
// Uses a vulnerability window to prevent instant kills on spawn.
// ============================================================

using UnityEngine;
using System.Collections;

public class Villano : MonoBehaviour
{
    [Header("Configuración")]
    public float distanciaSubida = 1.5f; // Vertical distance the enemy rises above its origin
    public float velocidad = 8f;          // Movement speed during rise and retreat animations
    public float tiempoEnPantalla = 1.5f; // Duration the enemy remains vulnerable at the top
    public int numeroDeEspacio = 1;       // Slot index; determines which player can hit this enemy

    private Vector3 posicionInicial; // Cached hidden (down) position set in Awake
    private Vector3 posicionArriba;  // Calculated visible (up) position
    private bool esVulnerable = false; // Vulnerability flag; only true during the active window

    /// <summary>
    /// Caches the local positions for the hidden and exposed states at startup.
    /// </summary>
    void Awake()
    {
        posicionInicial = transform.localPosition;
        // Calculate the target raised position relative to the initial position
        posicionArriba = posicionInicial + (Vector3.up * distanciaSubida);
    }

    /// <summary>
    /// Resets position and state every time the object is activated (object pooling support).
    /// Restarts the appearance coroutine to begin a fresh cycle.
    /// </summary>
    void OnEnable()
    {
        transform.localPosition = posicionInicial;
        esVulnerable = false; // Empieza siendo invulnerable
        StopAllCoroutines();
        StartCoroutine(CicloDeAparicion());
    }

    /// <summary>
    /// Coroutine that drives the full enemy lifecycle:
    /// rise → brief invulnerability grace period → vulnerability window → retreat → deactivate.
    /// </summary>
    IEnumerator CicloDeAparicion()
    {
        // 1. Rise to the exposed position
        yield return Mover(posicionArriba);
       
        // 2. Short grace period to avoid instant kills immediately after surfacing
        yield return new WaitForSeconds(0.1f);
        esVulnerable = true;
       
        // 3. Stay visible and vulnerable for the configured duration
        yield return new WaitForSeconds(tiempoEnPantalla);
       
        // 4. Disable vulnerability and retreat back to the hidden position
        esVulnerable = false;
        yield return Mover(posicionInicial);
        gameObject.SetActive(false); // Return to pool / deactivate
    }

    /// <summary>
    /// Coroutine that smoothly moves the enemy to a target local position using MoveTowards.
    /// Yields each frame until the destination is reached within a small tolerance.
    /// </summary>
    /// <param name="destino">Target local position to move toward.</param>
    IEnumerator Mover(Vector3 destino)
    {
        while (Vector3.Distance(transform.localPosition, destino) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destino, velocidad * Time.deltaTime);
            yield return null; // Wait one frame between each step
        }
        transform.localPosition = destino; // Snap to exact destination to avoid floating point drift
    }

    /// <summary>
    /// Physics2D trigger callback. Fires continuously while a collider overlaps this enemy.
    /// Checks vulnerability, correct player assignment, and active attack state before triggering death.
    /// </summary>
    /// <param name="other">The Collider2D that is currently overlapping this trigger.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        if (!esVulnerable) return; // Ignore all collisions during invulnerability window

        // Slot-based player assignment:
        // Spaces 1–4 belong to Player (tag "Player"); spaces 5+ belong to Player2 (tag "Player2")
        if ((numeroDeEspacio <= 4 && other.CompareTag("Player")) ||
            (numeroDeEspacio > 4 && other.CompareTag("Player2")))
        {
            // Retrieve attack components from the colliding object
            var p1 = other.GetComponent<Player1Attack>();
            var p2 = other.GetComponent<Player2Attack>();

            // Only register a hit if the overlapping player is actively attacking
            if ((p1 != null && p1.estaAtacando) || (p2 != null && p2.estaAtacando))
            {
                Morir(); // Trigger death sequence
            }
        }
    }

    /// <summary>
    /// Handles enemy death: disables vulnerability, awards a point to the owning GameManager,
    /// and deactivates this GameObject.
    /// </summary>
    void Morir()
    {
        esVulnerable = false;

        // Traverse the hierarchy upward to find the GameManager responsible for this enemy
        GameManager miManager = GetComponentInParent<GameManager>();

        if (miManager != null)
        {
            miManager.SumarPunto(); // Notify the manager to increment the score
        }

        gameObject.SetActive(false); // Deactivate enemy (supports object pooling)
    }
}