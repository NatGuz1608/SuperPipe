using UnityEngine;
using System.Collections;

/// <summary>
/// Controls a single villain (mole) in a Whack-a-Mole style game.
/// When activated from the pool by GameManager, the villain rises up,
/// becomes hittable for a limited time, then retreats and deactivates itself.
/// Reports hits back to its parent GameManager via SumarPunto().
/// </summary>
public class Villano : MonoBehaviour
{
    [Header("Configuration")]
    public float distanciaSubida = 1.5f;  // How far (in units) the villain rises above its initial position
    public float velocidad = 8f;          // Movement speed in units per second
    public float tiempoEnPantalla = 1.5f; // How long (in seconds) the villain stays visible and vulnerable
    public int numeroDeEspacio = 1;       // Slot index: 1-4 = Player 1 side, 5+ = Player 2 side

    private Vector3 posicionInicial; // Stored spawn position (hidden/down state)
    private Vector3 posicionArriba;  // Stored raised position (visible/up state)
    private bool esVulnerable = false; // Whether this villain can currently be hit

    void Awake()
    {
        // Cache the starting local position before any movement occurs
        posicionInicial = transform.localPosition;
        // Calculates the upward exit position
        posicionArriba = posicionInicial + (Vector3.up * distanciaSubida);
    }

    // Called every time this GameObject is re-enabled (i.e., spawned from the pool)
    void OnEnable()
    {
        // Reset position and state before starting the appearance cycle
        transform.localPosition = posicionInicial;
        esVulnerable = false; // Starts as invulnerable
        StopAllCoroutines();  // Cancel any leftover coroutines from a previous activation
        StartCoroutine(CicloDeAparicion());
    }

    /// <summary>
    /// Main lifecycle coroutine: rises up, becomes vulnerable, waits, then retreats and deactivates.
    /// </summary>
    IEnumerator CicloDeAparicion()
    {
        // 1. Moves up
        yield return Mover(posicionArriba);

        // 2. Waits a little before becoming vulnerable to avoid "instakill"
        yield return new WaitForSeconds(0.1f);
        esVulnerable = true; // Now the villain can be defeated by a player

        // 3. Stays up for the configured time
        yield return new WaitForSeconds(tiempoEnPantalla);

        // 4. Hides - time is up, villain retreats without being hit
        esVulnerable = false;
        yield return Mover(posicionInicial);
        gameObject.SetActive(false); // Return to pool; GameManager can reactivate later
    }

    /// <summary>
    /// Smoothly moves the villain's local position towards a destination each frame.
    /// </summary>
    /// <param name="destino">Target local position to move towards.</param>
    IEnumerator Mover(Vector3 destino)
    {
        // Keep moving until we are close enough to snap to the destination
        while (Vector3.Distance(transform.localPosition, destino) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destino, velocidad * Time.deltaTime);
            yield return null; // Wait one frame before continuing
        }
        // Snap exactly to destination to avoid floating-point drift
        transform.localPosition = destino;
    }

    /// <summary>
    /// Detects overlap with a player's collider each physics frame while contact persists.
    /// Only processes hits when the villain is in the vulnerable state.
    /// </summary>
    void OnTriggerStay2D(Collider2D other)
    {
        if (!esVulnerable) return; // Ignore all collisions when not vulnerable

        // Simplified logic: if someone attacks the villain, the villain dies
        // The "miManager" will be in charge of knowing who to give the point to
        // Space slots 1-4 belong to Player 1; slots 5+ belong to Player 2
        if ((numeroDeEspacio <= 4 && other.CompareTag("Player")) ||
            (numeroDeEspacio > 4 && other.CompareTag("Player2")))
        {
            // We validate if the touching player is attacking
            var p1 = other.GetComponent<Player1Attack>();
            var p2 = other.GetComponent<Player2Attack>();

            // Only register the hit if the correct player component is actively attacking
            if ((p1 != null && p1.estaAtacando) || (p2 != null && p2.estaAtacando))
            {
                Morir(); // Call with no parameters
            }
        }
    }

    /// <summary>
    /// Handles villain death: disables vulnerability, notifies the parent GameManager, and deactivates.
    /// </summary>
    void Morir()
    {
        esVulnerable = false; // Immediately prevent further hits during the death frame

        // Looks for the Manager that is a "parent" of this villain
        // Using GetComponentInParent ensures each villain reports to its own side's manager
        GameManager miManager = GetComponentInParent<GameManager>();

        if (miManager != null)
        {
            miManager.SumarPunto(); // Clean call - awards one point to the owning player
        }

        gameObject.SetActive(false); // Deactivate and return to pool
    }
}
