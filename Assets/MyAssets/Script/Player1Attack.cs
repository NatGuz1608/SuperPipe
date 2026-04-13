// ============================================================
// Player1Attack.cs
// Handles Player 1's attack input and sprite feedback.
// Player 1 uses WASD keys for directional attacks.
// Exposes a public flag (estaAtacando) that Villano.cs reads
// to validate whether a hit should be registered.
// ============================================================

using UnityEngine;

public class Player1Attack : MonoBehaviour
{
    [Header("Sprites de Movimiento")]
    public Sprite idle;    // Default sprite displayed when no attack key is held
    public Sprite ataqueW; // Arrastra attack_w_P1
    public Sprite ataqueA; // Arrastra attack_a_P1
    public Sprite ataqueS; // Arrastra attack_s_P1
    public Sprite ataqueD; // Arrastra attack_d_P1

    [HideInInspector] public bool estaAtacando = false; // Read by Villano.cs to confirm attack state
    private SpriteRenderer render; // Cached SpriteRenderer component

    /// <summary>
    /// Caches the SpriteRenderer reference once at initialization to avoid repeated GetComponent calls.
    /// </summary>
    void Awake() => render = GetComponent<SpriteRenderer>();

    /// <summary>
    /// Polls keyboard input every frame to update the attack direction sprite and attack state flag.
    /// Each WASD key corresponds to a directional attack animation sprite.
    /// Sets estaAtacando to false when no key is held (idle state).
    /// </summary>
    void Update()
    {
        estaAtacando = true; // Assume attacking; set false below if no key is held

        if (Input.GetKey(KeyCode.W))      render.sprite = ataqueW; // Up attack
        else if (Input.GetKey(KeyCode.A)) render.sprite = ataqueA; // Left attack
        else if (Input.GetKey(KeyCode.S)) render.sprite = ataqueS; // Down attack
        else if (Input.GetKey(KeyCode.D)) render.sprite = ataqueD; // Right attack
        else
        {
            // No attack key held — return to idle state
            render.sprite = idle;
            estaAtacando = false;
        }
    }
}