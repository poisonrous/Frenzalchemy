using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class anomatorolla : MonoBehaviour
{
    public Button botonPrincipal; // Botón que inicia la animación
    public GameObject objeto3D; // Objeto 3D para la animación
    public Button[] otrosBotones; // Otros botones que se deshabilitarán
    private Animator animator;

    void Start()
    {
        if (botonPrincipal == null)
        {
            Debug.LogError("El botón principal no está asignado.");
            return;
        }

        if (objeto3D == null)
        {
            Debug.LogError("El objeto 3D no está asignado.");
            return;
        }

        animator = objeto3D.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró el componente Animator en el objeto 3D.");
            return;
        }

        botonPrincipal.onClick.AddListener(IniciarAnimacion);
    }

    public void IniciarAnimacion()
    {
        animator.SetTrigger("Play");
        DeshabilitarBotones();
        float animDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("HabilitarBotones", animDuration);
    }

    void DeshabilitarBotones()
    {
        foreach (Button boton in otrosBotones)
        {
            boton.interactable = false;
        }
        botonPrincipal.interactable = false;
    }

    void HabilitarBotones()
    {
        foreach (Button boton in otrosBotones)
        {
            boton.interactable = true;
        }
        botonPrincipal.interactable = true;
    }
}

