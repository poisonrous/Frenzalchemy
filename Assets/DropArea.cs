using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour, IDropArea
{
    [SerializeField] private GameObject objectToSpawn;

    public void OnDrop(Draggable sample)
    {
        // Obtener el tipo de mezcla del sample
        string tipoMezcla = sample.tipoMezcla;
        Debug.Log("Tipo de mezcla recibido en DropArea: " + tipoMezcla); // Añade este log para verificar

        // Destruir el sample
        Destroy(sample.gameObject);

        // Instanciar el objeto burbuja y asignarle el tipo de mezcla
        GameObject burbuja = Instantiate(objectToSpawn, transform.position, transform.rotation);
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
