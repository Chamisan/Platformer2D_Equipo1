using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    public float jump_force;
    private Rigidbody2D body;
    public float move_force;

    public Animator animator;

    SpriteRenderer spi;

    private void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        spi = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("jumping", true);
            body.AddForce(Vector2.up * jump_force, ForceMode2D.Impulse);
        }
        //para que deje de saltar la animación
        if (Input.GetButtonUp("Jump")) 
        { 
            animator.SetBool("jumping", false); 
        }

        if (Input.GetAxis("Horizontal")!=0)
        {

            // Hace que el personaje gire cuando vaya a la izquierda
            if (Input.GetAxis("Horizontal") < 0)
                    {
                spi.flipX = true; }
            else { spi.flipX = false; }



            animator.SetBool("running", true);

            

            body.AddForce(Vector2.right * Input.GetAxis("Horizontal") * move_force * Time.deltaTime, ForceMode2D.Impulse);
            

        }
        //para que deje de correr la animación
        if (Input.GetAxis("Horizontal") == 0) { animator.SetBool("running", false); }
    }
}
