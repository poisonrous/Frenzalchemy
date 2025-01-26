using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbujaOptions : MonoBehaviour
{
    public float inflateRate = 0.1f; // Velocidad de inflado
    public GameObject outerBubble; // La burbuja negra externa
    public GameObject explosionEffect; // Efecto de explosión
    private bool isInflating = false;
    private bool isStopped = false; // Indica si la burbuja ha sido detenida

    void Update()
    {
        if (isStopped)
        {
            // Si la burbuja está detenida, no hacer nada
            return;
        }

        if (isInflating)
        {
            // Incrementar el tamaño de la burbuja
            transform.localScale += new Vector3(inflateRate, inflateRate, 0) * Time.deltaTime;
            if (transform.localScale.x > outerBubble.transform.localScale.x || transform.localScale.y > outerBubble.transform.localScale.y)
            {
                Explode();
            }
        }
        else
        {
            if (transform.localScale.x < 0.5 || transform.localScale.y < 0.5)
            {
                Explode();
            }
            // Decrementar el tamaño de la burbuja
            else
            {
                transform.localScale -= new Vector3(inflateRate, inflateRate, 0) * Time.deltaTime;
            }
        }

        // Detener la burbuja al presionar la tecla de espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isStopped = true;
        }
    }

    void OnMouseDown()
    {
        if (!isStopped)
        {
            isInflating = true;
        }
    }

    void OnMouseUp()
    {
        isInflating = false;
    }

    void Explode()
    {
        // Crear un efecto de explosión (opcional)
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Destruir la burbuja
        Destroy(gameObject);
    }
}
