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
        // Inicializar la lista con un par de mezclas
        for (int i = 0; i < 2; i++)
        {
            GameObject sample = Instantiate(samplePrefab);
            
            // Asignar un color diferente a cada mezcla
            if (i == 0)
            {
                sample.GetComponent<SpriteRenderer>().color = Color.red; // Mezcla roja
            }
            else if (i == 1)
            {
                sample.GetComponent<SpriteRenderer>().color = Color.blue; // Mezcla azul
            }

            samples.Add(sample);
        }

        // Colocar las mezclas en los compartimientos
        for (int i = 0; i < samples.Count; i++)
        {
            samples[i].transform.SetParent(compartments[i]);
            samples[i].transform.localPosition = Vector3.zero;
            samples[i].SetActive(true); // Mostrar la mezcla
        }
    }
}