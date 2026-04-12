/*Brenda Sofia Ramires Hernandez
Ingenieria multimedia: Programacion de videojuegos
2026*/

using UnityEngine;

public class Player2Attack : MonoBehaviour
{
    public Sprite idle;
    public Sprite attackUp;
    public Sprite attackDown;
    public Sprite attackLeft;
    public Sprite attackRight;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = idle;
    }

/*Controles del jugador (cambio de sprites) con las flechas*/

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            sr.sprite = attackUp;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            sr.sprite = attackDown;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sr.sprite = attackLeft;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            sr.sprite = attackRight;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) ||
            Input.GetKeyUp(KeyCode.DownArrow) ||
            Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.RightArrow))
        {
            sr.sprite = idle;
        }
    }
}
