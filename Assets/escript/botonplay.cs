using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class botonplay : MonoBehaviour
{
    public string Fabrica;
    public void CambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el objeto que colisiona tiene un tag específico (opcional)
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(Fabrica);
        }
    }

}
