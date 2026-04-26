using UnityEngine;

public class Player2Attack : MonoBehaviour
{
    public Sprite idle;
    public Sprite ataque1, ataque2, ataque3, ataque5;

    [HideInInspector] public bool estaAtacando = false;
    private SpriteRenderer render;

    void Awake() => render = GetComponent<SpriteRenderer>();

    void Update()
    {
        estaAtacando = true;

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
            render.sprite = idle;
            estaAtacando = false;
        }
    }
}