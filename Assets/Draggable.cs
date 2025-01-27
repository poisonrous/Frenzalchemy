using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    private Inventory inventory; // Referencia al inventario
    private UIManager uiManager; // Referencia a UIManager

    private void Start()
    {
        col = GetComponent<Collider2D>();
        inventory = FindObjectOfType<Inventory>(); // Encontrar el inventario en la escena
        uiManager = FindObjectOfType<UIManager>(); // Encontrar UIManager en la escena
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
            dropArea.OnDrop(this);
            string tipoMezcla = gameObject.name.Replace("Mezcla ", ""); // Suponiendo que el nombre del GameObject sea "Mezcla Buena", "Mezcla Media", "Mezcla Mala"
            if (uiManager != null)
            {
                uiManager.RestarMezcla(tipoMezcla); // Llama al método no estático
            }
            else
            {
                Debug.LogError("No se encontró la instancia de UIManager.");
            }
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