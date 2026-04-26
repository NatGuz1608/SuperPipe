using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalManager : MonoBehaviour
{
    [Header("UI de Victoria")]
    public GameObject panelFinal;        
    public TextMeshProUGUI textoGanador; 

    void Awake()
    {
 
        if (panelFinal != null) panelFinal.SetActive(false);
        Time.timeScale = 1f; 
    }

    public void DeclararGanador(string nombreDelGanador)
    {
        panelFinal.SetActive(true);
        textoGanador.text = "¡" + nombreDelGanador + " HA GANADO!";
        
       
        Time.timeScale = 0f; 
    }

   
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}