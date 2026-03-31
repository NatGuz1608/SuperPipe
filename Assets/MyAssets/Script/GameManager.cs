using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public List<GameObject> villanos; 
    public float intervaloAparicion = 2f;

    void Start()
    {
        CancelInvoke();
        InvokeRepeating("AparecerVillano", 1f, intervaloAparicion);
    }

    void AparecerVillano()
    {
        if (ContarActivos() > 0) return;

        List<GameObject> candidatos = new List<GameObject>();
        
        foreach (GameObject v in villanos)
        {
            if (v != null && !v.activeSelf)
            {
                candidatos.Add(v);
            }
        }

        if (candidatos.Count > 0)
        {
            int indiceAleatorio = Random.Range(0, candidatos.Count);
            candidatos[indiceAleatorio].SetActive(true);
        }
    }

    int ContarActivos()
    {
        int conteo = 0;
        foreach (GameObject v in villanos)
        {
            if (v != null && v.activeInHierarchy) conteo++;
        }
        return conteo;
    }
}