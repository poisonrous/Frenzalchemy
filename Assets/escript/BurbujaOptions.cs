using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbujaOptions : MonoBehaviour
{
    public float inflateRate = 0.1f; // Velocidad de inflado
    public GameObject outerBubble; // La burbuja negra externa
    public GameObject explosionEffect; // Efecto de explosi�n
    private bool isInflating = false;
    private bool isStopped = false; // Indica si la burbuja ha sido detenida
    private string tipoMezcla; // Tipo de mezcla

    void Start()
    {
        // Ajustar las propiedades del RectTransform
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.localScale = Vector3.one * 100;
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
    }

    void Update()
    {
        if (isStopped)
        {
            // Si la burbuja est� detenida, no hacer nada
            return;
        }

        if (isInflating)
        {
            // Incrementar el tama�o de la burbuja
            transform.localScale += new Vector3(inflateRate, inflateRate, inflateRate) * Time.deltaTime;
            if (transform.localScale.x > outerBubble.transform.localScale.x || transform.localScale.y > outerBubble.transform.localScale.y)
            {
                Explode();
            }
        }
        else
        {
            if (transform.localScale.x < 50 || transform.localScale.y < 50)
            {
                Explode();
            }
            // Decrementar el tama�o de la burbuja
            else
            {
                transform.localScale -= new Vector3(inflateRate, inflateRate, inflateRate) * Time.deltaTime;
            }
        }

        // Detener la burbuja al presionar la tecla de espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isStopped = true;
            Debug.Log("Tipo de mezcla en BurbujaOptions: " + tipoMezcla); // A�ade este log para verificar
            // Sumar o restar puntos seg�n el tipo de mezcla y las condiciones
            switch (tipoMezcla)
            {
                case "Buena":
                    float puntosBuena = 20 * (transform.localScale.x - 0.5f);
                    GameManager.Instance.SumarPuntos(Mathf.RoundToInt(puntosBuena)); // Sumar puntos para mezcla buena
                    break;
                case "Media":
                    float puntosMedia = 10 * (transform.localScale.x - 0.5f);
                    GameManager.Instance.SumarPuntos(Mathf.RoundToInt(puntosMedia)); // Sumar puntos para mezcla media
                    break;
                case "Mala":
                    GameManager.Instance.RestarPuntos(20); // Restar puntos para mezcla mala
                    break;
                default:
                    Debug.LogError("Tipo de mezcla no reconocido en BurbujaOptions: " + tipoMezcla);
                    break;
            }
        }
    }

    public void SetTipoMezcla(string tipo)
    {
        tipoMezcla = tipo;
        Debug.Log("SetTipoMezcla llamado con tipo: " + tipo); // A�ade este log para verificar
    }

    public string GetTipoMezcla()
    {
        return tipoMezcla;
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
        // Crear un efecto de explosi�n (opcional)
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Destruir la burbuja
        if (tipoMezcla == "Mala")
        {
            GameManager.Instance.RestarPuntos(20); // Restar puntos para mezcla mala si explota
        }
        Destroy(gameObject);
    }
}
