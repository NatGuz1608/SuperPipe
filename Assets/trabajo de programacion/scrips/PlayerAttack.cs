using UnityEngine;

public class Player1Attack : MonoBehaviour
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            sr.sprite = attackUp;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            sr.sprite = attackDown;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            sr.sprite = attackLeft;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            sr.sprite = attackRight;
        }

        if (Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.D))
        {
            sr.sprite = idle;
        }
    }
}