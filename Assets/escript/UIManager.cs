using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class UIManager : MonoBehaviour
{
    public TMP_Text textoCantidad;
    public Button botonDerecha;
    public Button botonIzquierda;
    public Button botonRestar;
    public Button botonMezclar;

    public TMP_Text textopiedraazul;
    public TMP_Text textopiedraroja;
    public TMP_Text textopiedragris;
    public TMP_Text textopiedrablanca;
    public TMP_Text textopiedraverde;

    public TMP_Text textonohaypararestar;
    public TMP_Text textonohayparamezclar;

    public Image piedraImagen;

    public TMP_Text listaRestadas; // Texto para mostrar la lista de cantidades restadas
    public TMP_Text mezclasBuena; // Texto para mostrar la cantidad de mezclas buena
    public TMP_Text mezclasMedia; // Texto para mostrar la cantidad de mezclas media
    public TMP_Text mezclasMala; // Texto para mostrar la cantidad de mezclas mala

    private int indicePiedraActual = 0;
    private int[] cantidadPiedra;
    private int[] cantidadRestada; // Cantidades restadas de cada tipo de piedra
    private Color[] coloresPiedras;

    // Variables estáticas para acumular la cantidad de mezclas hechas por tipo
    public static int cantidadMezclasBuena = 0;
    public static int cantidadMezclasMedia = 0;
    public static int cantidadMezclasMala = 0;

    void Start()
    {
        // Buscar y asignar el componente de imagen
        piedraImagen = GameObject.Find("PiedraImagen").GetComponent<Image>();
        if (piedraImagen == null)
        {
            Debug.LogError("No se encontró el componente Image en el objeto con nombre 'PiedraImagen'.");
            return;
        }

        // Definir los colores para cada tipo de piedra
        coloresPiedras = new Color[5];
        coloresPiedras[0] = Color.blue;
        coloresPiedras[1] = Color.red;
        coloresPiedras[2] = Color.green;
        coloresPiedras[3] = Color.white;
        coloresPiedras[4] = Color.gray;

        // Buscar y asignar el componente de texto por nombre en la jerarquía
        GameObject textoObject = GameObject.Find("PiedrasCantidad");
        if (textoObject != null)
        {
            textoCantidad = textoObject.GetComponent<TextMeshProUGUI>();
        }

        if (textoCantidad == null)
        {
           // Debug.LogError("No se encontró el componente TextMeshProUGUI en el objeto con nombre 'PiedrasCantidad'.");
            return;
        }

        // Buscar y asignar el componente de texto para la lista de restadas
        GameObject listaRestadasObject = GameObject.Find("ListaRestadas");
        if (listaRestadasObject != null)
        {
            listaRestadas = listaRestadasObject.GetComponent<TextMeshProUGUI>();
        }

        if (listaRestadas == null)
        {
            Debug.LogError("No se encontró el componente TextMeshProUGUI en el objeto con nombre 'ListaRestadas'.");
            return;
        }

        // Buscar y asignar los componentes de texto para las mezclas
        GameObject mezclasBuenaObject = GameObject.Find("MezclasBuena");
        if (mezclasBuenaObject != null)
        {
            mezclasBuena = mezclasBuenaObject.GetComponent<TextMeshProUGUI>();
        }

        if (mezclasBuena == null)
        {
            Debug.LogError("No se encontró el componente TextMeshProUGUI en el objeto con nombre 'MezclasBuena'.");
            return;
        }

        GameObject mezclasMediaObject = GameObject.Find("MezclasMedia");
        if (mezclasMediaObject != null)
        {
            mezclasMedia = mezclasMediaObject.GetComponent<TextMeshProUGUI>();
        }

        if (mezclasMedia == null)
        {
            Debug.LogError("No se encontró el componente TextMeshProUGUI en el objeto con nombre 'MezclasMedia'.");
            return;
        }

        GameObject mezclasMalaObject = GameObject.Find("MezclasMala");
        if (mezclasMalaObject != null)
        {
            mezclasMala = mezclasMalaObject.GetComponent<TextMeshProUGUI>();
        }

        if (mezclasMala == null)
        {
            Debug.LogError("No se encontró el componente TextMeshProUGUI en el objeto con nombre 'MezclasMala'.");
            return;
        }

        // Inicializa las cantidades de piedras desde managervariables
        cantidadPiedra = new int[5];
        cantidadPiedra[0] = managervariables.cantidadpiedraazul;
        cantidadPiedra[1] = managervariables.cantidadpiedraroja;
        cantidadPiedra[2] = managervariables.cantidadpiedraverde;
        cantidadPiedra[3] = managervariables.cantidadpiedrablanca;
        cantidadPiedra[4] = managervariables.cantidadpiedragris;

        // Inicializa las cantidades restadas
        cantidadRestada = new int[5];

        // Asigna los métodos a los botones
        GameObject botonDerechaObject = GameObject.Find("BotonDerecha");
        if (botonDerechaObject != null)
        {
            botonDerecha = botonDerechaObject.GetComponent<Button>();
        }

        GameObject botonIzquierdaObject = GameObject.Find("BotonIzquierda");
        if (botonIzquierdaObject != null)
        {
            botonIzquierda = botonIzquierdaObject.GetComponent<Button>();
        }

        GameObject botonRestarObject = GameObject.Find("BotonRestar");
        if (botonRestarObject != null)
        {
            botonRestar = botonRestarObject.GetComponent<Button>();
        }

        GameObject botonMezclarObject = GameObject.Find("BotonMezclar");
        if (botonMezclarObject != null)
        {
            botonMezclar = botonMezclarObject.GetComponent<Button>();
        }

        if (botonDerecha != null)
        {
            botonDerecha.onClick.AddListener(() => CambiarPiedra(1));
        }
        else
        {
            Debug.LogError("No se encontró el botón 'BotonDerecha'.");
        }

        if (botonIzquierda != null)
        {
            botonIzquierda.onClick.AddListener(() => CambiarPiedra(-1));
        }
        else
        {
            Debug.LogError("No se encontró el botón 'BotonIzquierda'.");
        }

        if (botonRestar != null)
        {
            botonRestar.onClick.AddListener(RestarPiedra);
        }
        else
        {
            Debug.LogError("No se encontró el botón 'BotonRestar'.");
        }

        if (botonMezclar != null)
        {
            botonMezclar.onClick.AddListener(MezclarPiedras);
        }
        else
        {
            Debug.LogError("No se encontró el botón 'BotonMezclar'.");
        }

        // Muestra la cantidad inicial
        void CambiarPiedra(int direccion)
        {
            indicePiedraActual = (indicePiedraActual + direccion + 5) % 5; // Asegura el rango de 0 a 4
            ActualizarUI();
        }

        void RestarPiedra()
        {
            if (cantidadPiedra[indicePiedraActual] > 0)
            {
                cantidadPiedra[indicePiedraActual]--;
                cantidadRestada[indicePiedraActual]++;
                ActualizarUI();
                ActualizarListaRestadas();
            }
            else
            {
                Debug.Log("No hay suficientes piedras para agregar.");
                // Uso de la corrutina MostrarMensaje
                StartCoroutine(MostrarMensaje("No hay suficientes piedras para agregar.", textonohaypararestar));
            }
        }

        void MezclarPiedras()
        {
            if (CantidadTotalRestada() == 0)
            {
                Debug.Log("No hay piedras en la lista restada para mezclar.");
                // Uso de la corrutina MostrarMensaje
                StartCoroutine(MostrarMensaje("No hay suficientes piedras para mezclar.", textonohayparamezclar));
                return;
            }

            // Determinar la calidad de la mezcla
            string calidadMezcla = DeterminarCalidadMezcla();

            // Actualizar el acumulador de mezclas
            switch (calidadMezcla)
            {
                case "Buena":
                    cantidadMezclasBuena++;
                    break;
                case "Media":
                    cantidadMezclasMedia++;
                    break;
                case "Mala":
                    cantidadMezclasMala++;
                    break;
            }

            // Reinicia la lista de piedras restadas
            for (int i = 0; i < cantidadRestada.Length; i++)
            {
                cantidadRestada[i] = 0;
            }

            ActualizarListaRestadas();
            ActualizarMezclas();
        }

        int CantidadTotalRestada()
        {
            int total = 0;
            for (int i = 0; i < cantidadRestada.Length; i++)
            {
                total += cantidadRestada[i];
            }
            return total;
        }
        string DeterminarCalidadMezcla()
        {
            int cantidadVerde = cantidadRestada[2];
            int cantidadBlanca = cantidadRestada[3];
            int cantidadAzul = cantidadRestada[0];
            int cantidadRoja = cantidadRestada[1];
            int cantidadGris = cantidadRestada[4];

            // Mezcla Buena: Al menos 5 piedras de cada una (verde, blanca, azul, roja)
            if (cantidadVerde >= 5 && cantidadBlanca >= 5 && cantidadAzul >= 5 && cantidadRoja >= 5)
            {
                return "Buena";
            }
            // Mezcla Media: Al menos 5 piedras rojas o azules, y al menos 3 piedras grises, y al menos 3 piedras verdes
            else if ((cantidadRoja >= 5 || cantidadAzul >= 5) && cantidadGris >= 3 && cantidadVerde >= 3)
            {
                return "Media";
            }
            // Mezcla Mala: Al menos 5 piedras rojas y al menos 5 piedras grises
            else if (cantidadRoja >= 5 && cantidadGris >= 5)
            {
                return "Mala";
            }

            return "Mala"; // Default a mezcla Mala si no cumple otros criterios
        }

        void ActualizarUI()
        {
            if (piedraImagen != null)
            {
                piedraImagen.color = coloresPiedras[indicePiedraActual];
            }
            if (textoCantidad != null)
            {
                string[] nombresPiedras = { "Azul", "Roja", "Verde", "Blanca", "Gris" };
               //mensaje de abajo color piedra................gggggggggggggggg
                textoCantidad.text = " " + nombresPiedras[indicePiedraActual] + ": \n" + cantidadPiedra[indicePiedraActual].ToString();
            }
        }

        void ActualizarListaRestadas()
        {
            if (listaRestadas != null)
            {
                string[] nombresPiedras = { "Azul", "Roja", "Verde", "Blanca", "Gris" };
                //lista de piedras escogidas texto
                listaRestadas.text = "Material Escogido\n\n";
                for (int i = 0; i < cantidadRestada.Length; i++)
                {
                    listaRestadas.text += nombresPiedras[i] + ": " + cantidadRestada[i].ToString() + "\n";

                    // Actualizar los textos individuales
                    switch (i)
                    {
                        case 0:
                            textopiedraazul.text = cantidadRestada[i].ToString();
                            break;
                        case 1:
                            textopiedraroja.text = cantidadRestada[i].ToString();
                            break;
                        case 2:
                            textopiedraverde.text = cantidadRestada[i].ToString();
                            break;
                        case 3:
                            textopiedrablanca.text = cantidadRestada[i].ToString();
                            break;
                        case 4:
                            textopiedragris.text = cantidadRestada[i].ToString();
                            break;
                    }
                }
            }
        }
    }

    private IEnumerator MostrarMensaje(string mensaje, TextMeshProUGUI texto)
    {
        texto.text = mensaje;
        yield return new WaitForSeconds(2f);
        texto.text = "";
    }
    void ActualizarMezclas()
        {
            if (mezclasBuena != null)
            {
                mezclasBuena.text = "Buena: " + cantidadMezclasBuena;
            }
            if (mezclasMedia != null)
            {
                mezclasMedia.text = "Media: " + cantidadMezclasMedia;
            }
            if (mezclasMala != null)
            {
                mezclasMala.text = "Mala: " + cantidadMezclasMala;
            }
    }
    private IEnumerator MostrarMensaje(string mensaje, TMP_Text texto)
    {
        texto.text = mensaje;
        yield return new WaitForSeconds(1f); // Espera 2 segundos
        texto.text = "";
    }
}







