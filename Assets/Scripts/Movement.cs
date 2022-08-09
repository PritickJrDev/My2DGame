using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed, jumpForce;
    public bool playerCanMove;

    [HideInInspector] public float direction;

    [HideInInspector] public bool playerIsGrounded;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatisGround;
    int noOfJumps, maxJumps = 0;

    Animator anim;
    public Animator dialogueAnim;
   


    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
       
    }

    private void Start()
    {
        moveSpeed = 0;
        jumpForce = 0;
        playerCanMove = true;
        
    }

    private void Update()
    {
  
        PlayerJump();
        WalkAnimation();
        TriggerPlayerMovement();
       
    }

    private void FixedUpdate()
    {

        playerIsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatisGround);
        PlayerMovement();
    }

    private void PlayerMovement()
    {

        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    private void PlayerJump()
    {
        if (playerIsGrounded)
        {
            noOfJumps = maxJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space) && noOfJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            noOfJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && noOfJumps == 0 && playerIsGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void WalkAnimation()
    {
        if(direction > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            anim.SetBool("walking", true);
        }
        else if(direction < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    public void TriggerPlayerMovement()
    {
        if (dialogueAnim.GetBool("isDialogueOpen") == false && playerCanMove)
        {
            moveSpeed = 3;
            direction = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("DeadCollider"))
    //    {
    //        gameObject.transform.position = new Vector2(-7f, 0);
    //    }
    //}
}
