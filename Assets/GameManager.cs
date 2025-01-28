using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton para que sea accesible desde cualquier script
    public int puntos = 50; // Iniciar el contador en 50
    public TMP_Text puntosTexto; // Referencia al texto de la UI para mostrar los puntos

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        EncontrarUIText();
        ActualizarUI();
    }

    private void OnEnable()
    {
        // Suscribirse al evento de cambio de escena
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Desuscribirse del evento de cambio de escena
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // Encontrar el objeto de texto en la nueva escena
        EncontrarUIText();
        ActualizarUI();
    }

    private void EncontrarUIText()
    {
        // Buscar el objeto de texto por tag
        GameObject textoObj = GameObject.FindGameObjectWithTag("UIText");
        if (textoObj != null)
        {
            puntosTexto = textoObj.GetComponent<TMP_Text>();
            if (puntosTexto == null)
            {
                Debug.LogError("El objeto de texto no tiene un componente TMP_Text.");
            }
        }
        else
        {
            Debug.LogError("No se encontró el objeto de texto con el tag 'UIText'.");
        }
    }

    // Método para sumar puntos
    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        ComprobarEstadoJuego();
        //ActualizarUI();
    }

    // Método para restar puntos
    public void RestarPuntos(int cantidad)
    {
        puntos -= cantidad;
        ComprobarEstadoJuego();
        //ActualizarUI();
    }

    // Actualiza el texto de la UI con los puntos actuales
    private void ActualizarUI()
    {
        if (puntosTexto != null)
        {
            puntosTexto.text = puntos.ToString();
            Debug.Log("Puntos actualizados: " + puntos); // Añade este log para verificar
        }
        else
        {
            Debug.LogError("puntosTexto es null. Asegúrate de que el objeto de texto está asignado en el Inspector.");
        }
    }

    // Comprueba si el juego se ha ganado o perdido
    private void ComprobarEstadoJuego()
    {
        if (puntos >= 100)
        {
            GanarJuego();
        }
        else if (puntos <= 0)
        {
            PerderJuego();
        }
    }

    private void GanarJuego()
    {
        Debug.Log("¡Has ganado el juego!");
        // TODO
    }

    private void PerderJuego()
    {
        Debug.Log("¡Has perdido el juego!");
        // TODO
    }
}
