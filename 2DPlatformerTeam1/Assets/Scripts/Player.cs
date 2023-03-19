using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    // *Nota: Cambié las variables con nombes en español a nombres en inglés y mejor hice comentarios del código en español
    private Rigidbody2D rb2d;

    [Header("Movement")]
    private float horizontalMovement = 0f;
    [SerializeField] private float movementSpeed;
    [Range(0, 0.3f)] [SerializeField] private float movementSmoothness; //Range es la parte que se puede elegir en el inpector con el puntito y esto es suavizado de movimiento
    private Vector3 velocity = Vector3.zero;
    private bool lookRight = true;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer; //Layer con que se hará contacto para saber si es suelo
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private Vector3 gizmosBoxDimensions; //Un vector que indica las dimensiones del gizmo que detectará el suelo
    [SerializeField] private bool isGrounded;
    private bool jump = false;

    //[Header("Points and Health")]

    //[Header("Animation")]

    private Vector3 startPosition; //Agregué para que vuelva a su posición inicial al morir

    [SerializeField] private GameObject winPanel; //Aquí agregaré el panel del ganador "Win" en el inspector
    

    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        startPosition = transform.position;  //Al iniciar que le de la posición a startPosition
        winPanel.SetActive(false); //Hay que tener al principio desactivado el panel del ganador
        //animator = GetComponent<Animator>();
        //Collision = false;
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed * 10;  //Sólo obtiene valor del movimiento horizontal

        if (Input.GetButtonDown("Jump"))  //Salto
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, gizmosBoxDimensions, 0.01f, groundLayer); //Checa si está en el suelo con el contacto que hace el gizmos con el layer que le asignemos para tierra en este caso Platform
        
        Move(horizontalMovement * Time.fixedDeltaTime, jump);  //Función de movimiento a la que se le asignan el valor horizontal y el bool de "salto"
        jump = false;
    }

    private void Move(float move, bool jump) //Función para moverse y saltar
    {
        Vector3 targetVelocity = new Vector2(move, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref velocity, movementSmoothness); //Esto controla el suavizado del movimiento pero no sé como... :s
        {
            if (move > 0 && !lookRight) //Si su movimiento es positivo y no ve a la derecha, entonces que de vuelta.
            {
                Turn();
            }
            if (move < 0 && lookRight) //Si su movimiento es negativo ve a la derecha, entonces que de vuelta.
            {
                Turn();
            }
            if (isGrounded && jump) //Si está en el suelo y salta:
            {
                isGrounded = false;   //No está en el suelo
                rb2d.AddForce(new Vector2(0f, jumpForce * 10)); //Se le da impulso de salto, le multipliqué por 10 porque necesita mucha fuerza
            }
        }
    }

    private void Turn() //Función que hace que de vuelta a la derecha o izquierda el player cambiando su escala en x con -1 usando el bool lookRight
    {
        lookRight = !lookRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos() //Dibuja el gizmos que hace contacto en el suelo con amarillo para que se pueda ver y manipular
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, gizmosBoxDimensions);
    }

    private void OnTriggerEnter2D(Collider2D collision)  
    {
        if (collision.gameObject.CompareTag("DeathZone")||collision.gameObject.CompareTag("Enemy")) //Agregué una condición donde si choca con esos tags se reinicie:
            StartCoroutine (Resurection()); //En esta corrutina de "Resurrección" se reinicia

        if (collision.gameObject.CompareTag("Finish")) //Si el personaje obtiene el objeto meta que tiene el tag Finish, gana :)
            Win();
    }


    private IEnumerator Resurection() //Esta es la corrutina que hace que el personaje reinicie posición
    {
        Time.timeScale = 0.04f; //Primero se alenta el juego
        yield return new WaitForSeconds(0.05f); //Tantito tiempo pasa la parte que se realenta
        transform.position = startPosition; //Para luego reiniciar su posición
        Time.timeScale = 1; //Y el tiempo de juego vuelve a la normalidad
    }

    private void Win() //Gana, activa el panel del ganador: Win y se pausa el juego
    {
        winPanel.SetActive(true);
        Time.timeScale = 0; 
    }
}
