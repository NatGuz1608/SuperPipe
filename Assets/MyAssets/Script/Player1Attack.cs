using UnityEngine;

/// <summary>
/// Handles Player 1's attack input and sprite changes.
/// Player 1 uses WASD keys - each direction maps to a unique attack sprite.
/// The estaAtacando flag is read by Villano every frame to validate whether a hit should register.
/// </summary>
public class Player1Attack : MonoBehaviour
{
    [Header("Movement Sprites")]
    public Sprite idle;     // Default sprite shown when no key is pressed
    public Sprite ataqueW;  // Drag attack_w_P1 - Up attack sprite
    public Sprite ataqueA;  // Drag attack_a_P1 - Left attack sprite
    public Sprite ataqueS;  // Drag attack_s_P1 - Down attack sprite
    public Sprite ataqueD;  // Drag attack_d_P1 - Right attack sprite

    [HideInInspector] public bool estaAtacando = false; // True while any attack key is held; read by Villano
    private SpriteRenderer render; // Cached reference to this object's SpriteRenderer

    // Cache the SpriteRenderer once at startup to avoid repeated GetComponent calls each frame
    void Awake() => render = GetComponent<SpriteRenderer>();

    /// <summary>
    /// Checks WASD input every frame.
    /// Sets estaAtacando to true and swaps the sprite when a direction key is held.
    /// Resets to idle state when no key is pressed.
    /// </summary>
    void Update()
    {
        estaAtacando = true; // Assume attacking; overridden below if no key is pressed

        if (Input.GetKey(KeyCode.W)) render.sprite = ataqueW;
        else if (Input.GetKey(KeyCode.A)) render.sprite = ataqueA;
        else if (Input.GetKey(KeyCode.S)) render.sprite = ataqueS;
        else if (Input.GetKey(KeyCode.D)) render.sprite = ataqueD;
        else
        {
            // No attack key held - return to idle and mark as not attacking
            render.sprite = idle;
            estaAtacando = false;
        }
    }
}
