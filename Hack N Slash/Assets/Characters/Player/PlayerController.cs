using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public ParticleSystem dust;

    //Player Components Refs
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundlayer;
    public TrailRenderer tr;
    public Animator animator;

    //Running Vars
    private bool canMove = true;
    private float horizontal;
    private float speed = 2f;
    private float jumpingpower = 6f;
    private bool isFacingRight = true;

    //Dashing Vars
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 5f;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 0.5f;

    //Rolling Vars
    private float rollSpeed = 3f;
    private bool canRoll = true;
    private bool isRolling;
    private float rollingTime = 0.1f;
    private float rollingCoolDown = 0.1f;


    
    void Start()
    {
        animator = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (isRolling)
        {
            return;
        }
        

        if (!isDashing)
        {
            animator.SetBool("Dash", false);
        }

        

        if (!isRolling)
        {
            animator.SetBool("Roll", false);
        }

        if(horizontal == 0)
        {
            animator.SetBool("isRunning", false);
        }

        if (IsGrounded())
        {
            animator.SetBool("Jump", false);
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
            
        }
        else if(isFacingRight && horizontal < 0f)
        {
            Flip();
            
        }

        animator.SetFloat("yVelocity", rb.velocity.y);
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingpower);
            animator.SetBool("Jump" , true);
        }

        if(context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.5f);
            animator.SetBool("Jump" , true);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        CreateDust();
    }

    public void Move(InputAction.CallbackContext context) 
    {
        if (canMove)
        {
            horizontal = context.ReadValue<Vector2>().x;
            animator.SetBool("isRunning", true);
        }
           
    }


    public void Dashing(InputAction.CallbackContext context)
    {
        if (canDash)
        {
            
            StartCoroutine(Dash());
            animator.SetBool("Dash", true);
        }
        
    }

    public void Rolling(InputAction.CallbackContext context)
    {
        if (canRoll && IsGrounded())
        {
            
            StartCoroutine(Roll());
            animator.SetBool("Roll", true);
        }
    }

    public void CreateDust()
    {
        dust.Play();
    }

  
    private IEnumerator Dash()
    {
        canRoll = false;
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
        canRoll = true;
    }

    private IEnumerator Roll()
    {
        canDash = false;
        canRoll = false;
        isRolling = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * rollSpeed, 0f);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(rollingTime);
       
        rb.gravityScale = originalGravity;
        isRolling = false;
        GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(rollingCoolDown);
        canRoll = true;
        canDash = true;
    }

   
}
