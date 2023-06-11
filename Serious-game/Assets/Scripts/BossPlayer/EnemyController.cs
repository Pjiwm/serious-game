using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float _health;

    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            healthbar.SetHealth((int)_health);
            if (_health <= 0)
            {
                Defeated();
            }
        }

    }

    public float maxHealth = 100;
    public Healthbar healthbar;
    private Animator animator;
    private Rigidbody2D rb;
    public DetectionZone detectionZone;
    public float moveSpeed = 0.7f;
    private bool canMove = true;

    private void Start()
    {
        Health = maxHealth;
        healthbar.SetMaxHealth((int)maxHealth);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        detectionZone = GetComponentInChildren<DetectionZone>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (detectionZone.detectedObjects.Count > 0)
            {
                animator.SetBool("IsMoving", true);
                Vector2 direction = detectionZone.detectedObjects[0].transform.position - transform.position;
                direction.Normalize();
                rb.velocity = direction * moveSpeed;
            }
            else
            {
                animator.SetBool("IsMoving", false);
                rb.velocity = Vector2.zero;
            }   
        }
    }

    public void Defeated()
    {
        animator.SetBool("IsAlive", false);
        healthbar.bar.gameObject.SetActive(false);
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void OnHit(float damage)
    {
        LockMovement();
        animator.SetTrigger("Hit");
        Health -= damage;
        Debug.Log(_health);
    }

    public void OnKnockBack(Vector2 direction)
    {
        rb.AddForce(direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BossPlayerController player = other.gameObject.GetComponent<BossPlayerController>();
        if (player != null)
        {
            player.OnHit(10);
        }
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


