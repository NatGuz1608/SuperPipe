// ============================================================
// Player2Attack.cs
// Handles Player 2's attack input and sprite feedback.
// Player 2 uses numeric keys (1, 2, 3, 5) to trigger attacks.
// Exposes a public flag (estaAtacando) that Villano.cs reads
// to validate whether a hit should be registered.
// ============================================================

using UnityEngine;

public class Player2Attack : MonoBehaviour
{
    public Sprite idle;                    // Default sprite when no attack key is held
    public Sprite ataque1, ataque2, ataque3, ataque5; // Attack sprites for keys 1, 2, 3 and 5

    [HideInInspector] public bool estaAtacando = false; // Read by Villano.cs to confirm attack state
    private SpriteRenderer render; // Cached SpriteRenderer component

    /// <summary>
    /// Caches the SpriteRenderer reference once at initialization to avoid repeated GetComponent calls.
    /// </summary>
    void Awake() => render = GetComponent<SpriteRenderer>();

    /// <summary>
    /// Polls keyboard input every frame to update the attack state and corresponding sprite.
    /// Accepts both top-row Alpha keys and Keypad keys for accessibility.
    /// Sets estaAtacando to false when no attack key is held (idle state).
    /// </summary>
    void Update()
    {
        estaAtacando = true; // Assume attacking; set false below if no key is held

        // Revisa tecla 1 (Alpha o Keypad)
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1)) render.sprite = ataque1;
        // Revisa tecla 2
        else if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2)) render.sprite = ataque2;
        // Revisa tecla 3
        else if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3)) render.sprite = ataque3;
        // Revisa tecla 5
        else if (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5)) render.sprite = ataque5;
        else
        {
            // No attack key held — return to idle state
            render.sprite = idle;
            estaAtacando = false;
        }
    }
}