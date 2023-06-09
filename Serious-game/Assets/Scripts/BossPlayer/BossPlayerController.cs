using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossPlayerController : MonoBehaviour
{
    private Vector2 movementInput;
    private Rigidbody2D rb;
    private List<RaycastHit2D> castColissions = new List<RaycastHit2D>();
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool canMove = true;
    
    public float collisionOffset = 0.05f;
    public float movespeed = 1f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    

    private void FixedUpdate()
    {
        if (canMove)
        {
            // if movement is not zero, try to move
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success && movementInput.x != 0)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                
                }
                if (!success && movementInput.y != 0)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
                animator.SetBool("IsMoving", success);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }

            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    void OnMovement(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
                direction,
                movementFilter,
                castColissions,
                movespeed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * movespeed * Time.fixedDeltaTime);
            return true;
        }
        else 
        { 
            return false;
        }
    }

    void OnAttack()
    {
        animator.SetTrigger("SwordAttack");
    }
    
    public void SwordAttack()
    {
        LockMovement();
        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }

    }
    
    public void EndSwordAttack()
    {
        swordAttack.StopAttack();
        UnlockMovement();
    }
    
    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}
