using UnityEngine;
using System.Collections;

public class Villano : MonoBehaviour
{
    [Header("Configuración")]
    public float distanciaSubida = 1.5f; 
    public float velocidad = 8f; 
    public float tiempoEnPantalla = 1.5f;
    public int numeroDeEspacio = 1; 

    private Vector3 posicionInicial;
    private Vector3 posicionArriba;
    private bool esVulnerable = false;

    void Awake()
    {
        posicionInicial = transform.localPosition;
        // Calcula la posición de salida hacia arriba
        posicionArriba = posicionInicial + (Vector3.up * distanciaSubida);
    }

    void OnEnable()
    {
        transform.localPosition = posicionInicial;
        esVulnerable = false; // Empieza siendo invulnerable
        StopAllCoroutines();
        StartCoroutine(CicloDeAparicion());
    }

    IEnumerator CicloDeAparicion()
    {
        // 1. Sube
        yield return Mover(posicionArriba);
        
        // 2. Espera un poquito antes de ser vulnerable para evitar el "instakill"
        yield return new WaitForSeconds(0.1f);
        esVulnerable = true;
        
        // 3. Se queda arriba el tiempo configurado
        yield return new WaitForSeconds(tiempoEnPantalla);
        
        // 4. Se esconde
        esVulnerable = false;
        yield return Mover(posicionInicial);
        gameObject.SetActive(false);
    }

    IEnumerator Mover(Vector3 destino)
    {
        while (Vector3.Distance(transform.localPosition, destino) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destino, velocidad * Time.deltaTime);
            yield return null;
        }
        transform.localPosition = destino;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!esVulnerable) return;

            // Lógica simplificada: si alguien ataca al villano, el villano muere
            // El "miManager" se encargará de saber a quién darle el punto
            if ((numeroDeEspacio <= 4 && other.CompareTag("Player")) || 
                (numeroDeEspacio > 4 && other.CompareTag("Player2")))
            {
                // Validamos si el jugador que toca está atacando
                var p1 = other.GetComponent<Player1Attack>();
                var p2 = other.GetComponent<Player2Attack>();

                if ((p1 != null && p1.estaAtacando) || (p2 != null && p2.estaAtacando))
                {
                    Morir(); // Llamada sin parámetros
                }
            }
    }

    void Morir()
        {
            esVulnerable = false;
                
                // Busca al Manager que sea "pariente" de este villano
                GameManager miManager = GetComponentInParent<GameManager>();
                
                if (miManager != null)
                {
                    miManager.SumarPunto(); // Llamada limpia
                }
                
                gameObject.SetActive(false);
        }
}