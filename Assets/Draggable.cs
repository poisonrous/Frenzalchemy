using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector3 startDragPosition;
    private Inventory inventory; // Referencia al inventario
    public string tipoMezcla; // Propiedad para almacenar el tipo de mezcla
    private Canvas canvas; // Referencia al Canvas

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>(); // Encontrar el inventario en la escena
        tipoMezcla = gameObject.name.Replace("Mezcla ", ""); // Asignar el tipo de mezcla basado en el nombre del GameObject
        canvas = FindObjectOfType<Canvas>(); // Encontrar el Canvas en la escena

        if (canvas == null)
        {
            Debug.LogError("No se encontró el Canvas en la escena.");
        }
        else
        {
            Debug.Log("Canvas encontrado en Start: " + canvas.name); // Añadir log para verificar
        }

        Debug.Log("Tipo de mezcla asignado en Draggable: " + tipoMezcla); // Añade este log para verificar
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        startDragPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        transform.position = GetMousePositionInWorldSpace();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        Vector3 worldPoint = GetMousePositionInWorldSpace();
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPoint);

        if (hitCollider != null)
        {
            Debug.Log("Hit Collider: " + hitCollider.gameObject.name); // Añade este log para verificar el nombre del collider golpeado
            if (hitCollider.TryGetComponent(out IDropArea dropArea))
            {
                Debug.Log("Draggable: Tipo de mezcla antes de OnDrop: " + tipoMezcla); // Añade este log para verificar
                dropArea.OnDrop(this);
            }
        }
        else
        {
            Debug.Log("No se detectó ningún Collider en la posición del mouse.");
            transform.position = startDragPosition;
        }
    }

    public Vector3 GetMousePositionInWorldSpace()
    {
        if (canvas == null)
        {
            Debug.LogError("Canvas es null en GetMousePositionInWorldSpace.");
            // Intentar encontrar el canvas si es null
            canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("No se pudo encontrar Canvas en GetMousePositionInWorldSpace.");
                return Vector3.zero;
            }
            else
            {
                Debug.Log("Canvas encontrado en GetMousePositionInWorldSpace: " + canvas.name);
            }
        }

        Vector2 screenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, screenPoint, canvas.worldCamera, out Vector3 worldPoint);
        return worldPoint;
    }
}
