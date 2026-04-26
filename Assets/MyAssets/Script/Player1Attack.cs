using UnityEngine;

public class Player1Attack : MonoBehaviour
{
    [Header("Sprites de Movimiento")]
    public Sprite idle;
    public Sprite ataqueW; // Arrastra attack_w_P1
    public Sprite ataqueA; // Arrastra attack_a_P1
    public Sprite ataqueS; // Arrastra attack_s_P1
    public Sprite ataqueD; // Arrastra attack_d_P1

    [HideInInspector] public bool estaAtacando = false;
    private SpriteRenderer render;

    void Awake() => render = GetComponent<SpriteRenderer>();

    void Update()
    {
        estaAtacando = true;

        if (Input.GetKey(KeyCode.W)) render.sprite = ataqueW;
        else if (Input.GetKey(KeyCode.A)) render.sprite = ataqueA;
        else if (Input.GetKey(KeyCode.S)) render.sprite = ataqueS;
        else if (Input.GetKey(KeyCode.D)) render.sprite = ataqueD;
        else
        {
            render.sprite = idle;
            estaAtacando = false;
        }
    }
}