using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<GameObject> samples = new List<GameObject>(); // Lista inicializada con mezclas
    public int maxSamples = 3; // Máximo de muestras permitido
    public GameObject samplePrefab;
    public Transform[] compartments; // Referencias a los compartimientos de la repisa

    void Start()
    {
        ActualizarInventarioConMezclas();

        // Colocar las mezclas en los compartimientos
        ColocarMezclasEnCompartimientos();
    }

    void ActualizarInventarioConMezclas()
    {
        // Obtener las cantidades de las mezclas desde UIManager
        int cantidadMezclasBuena = UIManager.cantidadMezclasBuena;
        int cantidadMezclasMedia = UIManager.cantidadMezclasMedia;
        int cantidadMezclasMala = UIManager.cantidadMezclasMala;

        // Crear las mezclas según las cantidades
        CrearMezclas(cantidadMezclasBuena, Color.green, "Buena");
        CrearMezclas(cantidadMezclasMedia, Color.yellow, "Media");
        CrearMezclas(cantidadMezclasMala, Color.red, "Mala");
    }

    void CrearMezclas(int cantidad, Color color, string tipo)
    {
        for (int i = 0; i < cantidad; i++)
        {
            if (samples.Count >= maxSamples * compartments.Length) break; // Limitar el número de muestras

            GameObject sample = Instantiate(samplePrefab);
            sample.GetComponent<SpriteRenderer>().color = color; // Asignar color a la mezcla
            sample.name = "Mezcla " + tipo;
            samples.Add(sample);
        }
    }

    void ColocarMezclasEnCompartimientos()
    {
        // Diccionario para agrupar las mezclas por tipo
        Dictionary<string, List<GameObject>> mezclasPorTipo = new Dictionary<string, List<GameObject>>();
        
        // Agrupar las mezclas por tipo
        foreach (GameObject sample in samples)
        {
            string tipoMezcla = sample.name.Replace("Mezcla ", "");
            if (!mezclasPorTipo.ContainsKey(tipoMezcla))
            {
                mezclasPorTipo[tipoMezcla] = new List<GameObject>();
            }
            mezclasPorTipo[tipoMezcla].Add(sample);
        }

        // Colocar las mezclas en los compartimientos adecuados
        foreach (var tipoMezcla in mezclasPorTipo)
        {
            List<GameObject> mezclas = tipoMezcla.Value;
            Transform compartimiento = ObtenerCompartimientoPorTipo(tipoMezcla.Key);

            foreach (GameObject sample in mezclas)
            {
                sample.transform.SetParent(compartimiento);
                sample.transform.localPosition = Vector3.zero;
                sample.SetActive(true); // Mostrar la mezcla
            }
        }
    }

    Transform ObtenerCompartimientoPorTipo(string tipo)
    {
        // Asumiendo que tienes una manera de identificar los compartimientos
        switch (tipo)
        {
            case "Buena":
                return compartments[0];
            case "Media":
                return compartments[1];
            case "Mala":
                return compartments[2];
            default:
                return null;
        }
    }
}

