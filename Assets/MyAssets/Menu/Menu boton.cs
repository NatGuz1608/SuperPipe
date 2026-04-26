using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuboton : MonoBehaviour
{
    public AudioSource fuenteSonido;
    public AudioClip sonidoClick;

    public void Jugar()
    {
        // 1. Primero suena
        fuenteSonido.PlayOneShot(sonidoClick);
        
        // 2. Luego esperamos 0.2 segundos para cambiar de escena
        Invoke("CargarSiguienteEscena", 0.2f);
    }

    void CargarSiguienteEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        fuenteSonido.PlayOneShot(sonidoClick);
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}