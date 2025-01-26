using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AreaTrigger : MonoBehaviour
{

    public TextMeshPro textMeshPro; // Objeto de texto público
    public SpriteRenderer barraCircular; // Sprite circular público
    public SpriteRenderer fondoCircular; // Sprite del fondo circular público
    public float tiempoParaObtenerPiedra = 2.0f; // Tiempo necesario para obtener una piedra
    public string colorPiedra; // Color de la piedra que se va a otorgar
    public float tamanoFinal = 1.0f; // Tamaño final del sprite de la esfera
    private bool jugadorDentro = false;
    private float tiempoDentro = 0.0f;
    private bool clickMantenido = false;

    void Start()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("No se encontró el componente TextMeshPro asignado.");
        }
        else
        {
            textMeshPro.gameObject.SetActive(false); // Ocultar el texto al inicio
        }

        if (barraCircular == null)
        {
            Debug.LogError("No se encontró el componente SpriteRenderer asignado para la barra circular.");
        }
        else
        {
            barraCircular.gameObject.SetActive(false); // Ocultar la barra circular al inicio
        }

        if (fondoCircular == null)
        {
            Debug.LogError("No se encontró el componente SpriteRenderer asignado para el fondo circular.");
        }
        else
        {
            fondoCircular.gameObject.SetActive(false); // Ocultar el fondo circular al inicio
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (textMeshPro != null)
            {
                textMeshPro.gameObject.SetActive(true); // Mostrar el texto
            }
            jugadorDentro = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (textMeshPro != null)
            {
                textMeshPro.gameObject.SetActive(false); // Ocultar el texto
            }
            if (barraCircular != null)
            {
                barraCircular.gameObject.SetActive(false); // Ocultar la barra circular
            }
            if (fondoCircular != null)
            {
                fondoCircular.gameObject.SetActive(false); // Ocultar el fondo circular
            }
            jugadorDentro = false;
            tiempoDentro = 0.0f;
            clickMantenido = false;
        }
    }

    void Update()
    {
        if (jugadorDentro)
        {
            if (Input.GetMouseButton(0)) // Mantener el clic presionado
            {
                clickMantenido = true;
                tiempoDentro += Time.deltaTime;
                if (barraCircular != null && fondoCircular != null)
                {
                    barraCircular.gameObject.SetActive(true); // Mostrar la barra circular
                    fondoCircular.gameObject.SetActive(true); // Mostrar el fondo circular
                    float escala = (tiempoDentro / tiempoParaObtenerPiedra) * tamanoFinal;
                    barraCircular.transform.localScale = new Vector3(escala, escala, 1); // Actualizar el tamaño según el progreso
                }

                if (tiempoDentro >= tiempoParaObtenerPiedra)
                {
                    OtorgarPiedra();
                    tiempoDentro = 0.0f; // Reinicia el temporizador
                    clickMantenido = false; // Reinicia el estado del clic
                    if (barraCircular != null && fondoCircular != null)
                    {
                        barraCircular.gameObject.SetActive(false); // Ocultar la barra circular
                        fondoCircular.gameObject.SetActive(false); // Ocultar el fondo circular
                        barraCircular.transform.localScale = Vector3.zero; // Reiniciar el tamaño
                    }
                }
            }
            else if (clickMantenido && !Input.GetMouseButton(0)) // Si se suelta el clic antes del tiempo
            {
                tiempoDentro = 0.0f;
                clickMantenido = false;
                if (barraCircular != null && fondoCircular != null)
                {
                    barraCircular.gameObject.SetActive(false); // Ocultar la barra circular
                    fondoCircular.gameObject.SetActive(false); // Ocultar el fondo circular
                    barraCircular.transform.localScale = Vector3.zero; // Reiniciar el tamaño
                }
            }
        }
    }

    void OtorgarPiedra()
    {
        Debug.Log("Piedra obtenida!");
        managervariables.AgregarPiedra(colorPiedra, 1);
    }
}