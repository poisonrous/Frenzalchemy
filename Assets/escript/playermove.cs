using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playermove : MonoBehaviour
{
    public float speed = 20f; // Velocidad de movimiento
   

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.velocity = movement * speed;
    }
 
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("piedraazul"))
            {
                managervariables.AgregarPiedra("azul",1);
            }
            else if (other.gameObject.CompareTag("piedraroja"))
            {
                managervariables.AgregarPiedra("roja", 1);
            }
            else if (other.gameObject.CompareTag("piedraverde"))
            {
                managervariables.AgregarPiedra("verde", 1);
            }
            else if (other.gameObject.CompareTag("piedrablanca"))
            {
                managervariables.AgregarPiedra("blanca", 1);
            }
            else if (other.gameObject.CompareTag("piedragris"))
            {
                managervariables.AgregarPiedra("gris", 1);
            }
        }
    }
