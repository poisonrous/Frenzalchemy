using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class managervariables : MonoBehaviour
{
    public static int cantidadpiedraazul = 0;
    public static int cantidadpiedraroja = 0;
    public static int cantidadpiedraverde = 0;
    public static int cantidadpiedrablanca = 0;
    public static int cantidadpiedragris = 0;

    public static bool tienepiedraazul = false;
    public static bool tienepiedraroja = false;
    public static bool tienepiedraverde = false;
    public static bool tienepiedrablanca = false;
    public static bool tienepiedragris = false;

    public static void AgregarPiedra(string tipopiedra, int cantidad)
    {
        switch (tipopiedra)
        {
            case "azul":
                cantidadpiedraazul += cantidad;
                tienepiedraazul = cantidadpiedraazul > 0;
                Debug.Log("Piedra azul. Cantidad: " + cantidadpiedraazul);
                break;
            case "roja":
                cantidadpiedraroja += cantidad;
                tienepiedraroja = cantidadpiedraroja > 0;
                Debug.Log("Piedra roja. Cantidad: " + cantidadpiedraroja);
                break;
            case "verde":
                cantidadpiedraverde += cantidad;
                tienepiedraverde = cantidadpiedraverde > 0;
                Debug.Log("Piedra verde. Cantidad: " + cantidadpiedraverde);
                break;
            case "blanca":
                cantidadpiedrablanca += cantidad;
                tienepiedrablanca = cantidadpiedrablanca > 0;
                Debug.Log("Piedra blanca. Cantidad: " + cantidadpiedrablanca);
                break;
            case "gris":
                cantidadpiedragris += cantidad;
                tienepiedragris = cantidadpiedragris > 0;
                Debug.Log("Piedra gris. Cantidad: " + cantidadpiedragris);
                break;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
