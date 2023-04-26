using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //Compenent references
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    //movement var
    public float speed = 5.0f;
    public float jumpForce = 300.0f;

    //groundcheck stuff
    public bool isGrounded;
    public Transform GroundCheck;
    public LayerMask isGroundedLayer;
    public float groundCheckRadius = 0.02f;


    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       sr = GetComponent<SpriteRenderer>();
       anim = GetComponent<Animator>();

    //protects against bad input
        if (speed <= 0) speed = 5.0f;
        if (jumpForce <= 0) jumpForce = 300.0f;
        if (groundCheckRadius <= 0) groundCheckRadius = 0.02f;

        if (!GroundCheck) GroundCheck = GameObject.FindGameObjectWithTag("GroundCheck").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       float hinput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, isGroundedLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);

        }

        Vector2 moveDirection = new Vector2(hinput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetFloat("hinput", Mathf.Abs(hinput));
        anim.SetBool("isGrounded", isGrounded); 
   
    
    }
}
