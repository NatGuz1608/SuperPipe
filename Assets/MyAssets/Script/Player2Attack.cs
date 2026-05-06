using UnityEngine;

/// <summary>
/// Handles Player 2's attack input and sprite changes.
/// Player 2 uses number keys 1, 2, 3, and 5 (both Alpha row and Numpad are supported
/// to accommodate different keyboard layouts). Key 4 is intentionally unused.
/// The estaAtacando flag is read by Villano every frame to validate whether a hit should register.
/// </summary>
public class Player2Attack : MonoBehaviour
{
    public Sprite idle;                              // Default sprite shown when no key is pressed
    public Sprite ataque1, ataque2, ataque3, ataque5; // Attack sprites for keys 1, 2, 3, and 5

    [HideInInspector] public bool estaAtacando = false; // True while any attack key is held; read by Villano
    private SpriteRenderer render; // Cached reference to this object's SpriteRenderer

    // Cache the SpriteRenderer once at startup to avoid repeated GetComponent calls each frame
    void Awake() => render = GetComponent<SpriteRenderer>();

    /// <summary>
    /// Checks number key input every frame.
    /// Accepts both the Alpha row and the Numpad for each key to support different keyboard layouts.
    /// Sets estaAtacando to true and swaps the sprite when a key is held.
    /// Resets to idle state when no key is pressed.
    /// </summary>
    void Update()
    {
        estaAtacando = true; // Assume attacking; overridden below if no key is pressed

        // Check key 1 (Alpha or Keypad)
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1)) render.sprite = ataque1;
        // Check key 2
        else if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2)) render.sprite = ataque2;
        // Check key 3
        else if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3)) render.sprite = ataque3;
        // Check key 5 (key 4 is intentionally skipped)
        else if (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5)) render.sprite = ataque5;
        else
        {
            // No attack key held - return to idle and mark as not attacking
            render.sprite = idle;
            estaAtacando = false;
        }
    }
