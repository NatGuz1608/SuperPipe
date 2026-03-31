using UnityEngine;
using System.Collections;

public partial class Villano : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float distanciaSubida = 1.0f; 
    public float velocidad = 5f; 
    public float tiempoEnPantalla = 1.5f;

    private Vector3 posicionInicial;
    private Vector3 posicionArriba;

    void Awake()
    {
        posicionInicial = transform.localPosition;
        Vector3 direccionMundo = transform.TransformDirection(Vector3.up);
        float direccionFinalY = direccionMundo.y;

        if (direccionFinalY < 0)
        {
            posicionArriba = posicionInicial + new Vector3(0, -distanciaSubida, 0);
        }
        else
        {
            posicionArriba = posicionInicial + new Vector3(0, distanciaSubida, 0);
        }
        // -----------------------------
    }

    void OnEnable()
    {
        transform.localPosition = posicionInicial; 
        StopAllCoroutines();
        StartCoroutine(CicloDeAparicion());
    }

    IEnumerator CicloDeAparicion()
    {
        // 1. APARECER (Moverse al escenario)
        yield return MoverPersonaje(posicionArriba);

        // 2. ESPERAR
        yield return new WaitForSeconds(tiempoEnPantalla);

        // 3. OCULTARSE (Regresar a la posición inicial)
        yield return MoverPersonaje(posicionInicial);

        // 4. DESACTIVAR
        gameObject.SetActive(false);
    }


    IEnumerator MoverPersonaje(Vector3 destino)
    {
        while (transform.localPosition != destino)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition, 
                destino, 
                velocidad * Time.deltaTime
            );
            yield return null;
        }
        transform.localPosition = destino;
    }

    void OnMouseDown()
    {
        gameObject.SetActive(false);
    }
}