using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour, IDropArea
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Canvas canvas; // Referencia al Canvas

    private void Start()
    {
        // Intentar encontrar el Canvas si no está asignado en el Inspector
        if (canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("No se encontró el Canvas en la escena.");
            }
        }
    }

    public void OnDrop(Draggable sample)
    {
        // Obtener el tipo de mezcla del sample
        string tipoMezcla = sample.tipoMezcla;
        Debug.Log("Tipo de mezcla recibido en DropArea: " + tipoMezcla); // Añade este log para verificar

        // Destruir el sample
        Destroy(sample.gameObject);

        // Instanciar el objeto burbuja y asignarle el tipo de mezcla
        GameObject burbuja = Instantiate(objectToSpawn);

        // Establecer la burbuja como hija del Canvas
        burbuja.transform.SetParent(canvas.transform, false);

        // Ajustar la escala inicial de la burbuja a 100
        burbuja.transform.localScale = new Vector3(100, 100, 100);

        // Asegurarse de que las propiedades del RectTransform sean correctas
        RectTransform burbujaRect = burbuja.GetComponent<RectTransform>();
        if (burbujaRect != null)
        {
            burbujaRect.localScale = new Vector3(100, 100, 100);
            burbujaRect.anchorMin = new Vector2(0.5f, 0.5f);
            burbujaRect.anchorMax = new Vector2(0.5f, 0.5f);
            burbujaRect.pivot = new Vector2(0.5f, 0.5f);
        }

        // Ajustar la posición de la burbuja al centro del DropArea
        RectTransform dropAreaRect = GetComponent<RectTransform>();
        if (dropAreaRect != null && burbujaRect != null)
        {
            burbujaRect.anchoredPosition = dropAreaRect.anchoredPosition;
            burbujaRect.localPosition = Vector3.zero;
        }

        BurbujaOptions burbujaOptions = burbuja.GetComponent<BurbujaOptions>();
        if (burbujaOptions != null)
        {
            burbujaOptions.SetTipoMezcla(tipoMezcla);
            Debug.Log("Tipo de mezcla asignado en BurbujaOptions: " + burbujaOptions.GetTipoMezcla()); // Añade este log para verificar
        }
        else
        {
            Debug.LogError("No se encontró el componente BurbujaOptions en el objeto instanciado.");
        }
    }
}
