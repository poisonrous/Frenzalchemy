using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piedrasmanager : MonoBehaviour
{
   
    // Intervalo de tiempo para que la piedra vuelva a aparecer (en segundos)
    public float tiempoDesaparicion = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            string tagPiedra = gameObject.tag;



            // Desactiva el objeto de la piedra y lo registra en la consola
            //Debug.Log("Piedra " + gameObject.tag + " desaparecida en tiempo: " + Time.time);
            gameObject.SetActive(false);

            // Invoca el método ReaparecerPiedra después de un tiempo especificado
            Invoke("ReaparecerPiedra", tiempoDesaparicion);
        }
    }

    void ReaparecerPiedra()
    {
        // Reactiva el objeto de la piedra y lo registra en la consola
        gameObject.SetActive(true);
        //Debug.Log("Piedra " + gameObject.tag + " reaparecida en tiempo: " + Time.time);



    }
}
  
