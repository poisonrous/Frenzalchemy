using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    private Inventory inventory; // Referencia al inventario
    public string tipoMezcla; // Propiedad para almacenar el tipo de mezcla

    private void Start()
    {
        col = GetComponent<Collider2D>();
        inventory = FindObjectOfType<Inventory>(); // Encontrar el inventario en la escena
        tipoMezcla = gameObject.name.Replace("Mezcla ", ""); // Asignar el tipo de mezcla basado en el nombre del GameObject
        Debug.Log("Tipo de mezcla asignado en Draggable: " + tipoMezcla); // Añade este log para verificar
    }

    private void OnMouseDown()
    {
        startDragPosition = transform.position;
        transform.position = GetMousePositionInWorldSpace();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePositionInWorldSpace();
    }

    private void OnMouseUp()
    {
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;
        if (hitCollider != null && hitCollider.TryGetComponent(out IDropArea dropArea))
        {
            Debug.Log("Draggable: Tipo de mezcla antes de OnDrop: " + tipoMezcla); // Añade este log para verificar
            dropArea.OnDrop(this);
        }
        else
        {
            transform.position = startDragPosition;
        }
    }

    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }
}
