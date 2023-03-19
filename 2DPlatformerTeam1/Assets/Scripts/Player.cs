using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [Header("Movement")]
    private float horizontalMovement = 0f;
    [SerializeField] private float movementSpeed;
    [Range(0, 0.3f)] [SerializeField] private float movementSmoothness;
    private Vector3 velocity = Vector3.zero;
    private bool lookRight = true;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask IsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector3 gizmosBoxDimensions;
    [SerializeField] private bool isGrounded;
    private bool jump = false;

    //[Header("Points and Health")]

    //[Header("Animation")]

    //Game over
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        //startPosition = transform.position;
        //animator = GetComponent<Animator>();
        //Collision = false;
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed * 10;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, gizmosBoxDimensions, 0.01f, IsGround);
        //Move
        Move(horizontalMovement * Time.fixedDeltaTime, jump);
        jump = false;
    }

    private void Move(float move, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref velocity, movementSmoothness);
        {
            if (move > 0 && !lookRight)
            {
                //Turn
                Turn();
            }
            if (move < 0 && lookRight)
            {
                //Turn
                Turn();
            }
            if (isGrounded && jump)
            {
                isGrounded = false;
                rb2d.AddForce(new Vector2(0f, jumpForce * 10));
            }
        }
    }

    private void Turn()
    {
        lookRight = !lookRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, gizmosBoxDimensions);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone")||collision.gameObject.CompareTag("Enemy"))
            StartCoroutine (Resurection());
    }


    private IEnumerator Resurection()
    {
        Time.timeScale = 0.04f;
        yield return new WaitForSeconds(0.05f);
        transform.position = startPosition;
        Time.timeScale = 1;
    }
}
