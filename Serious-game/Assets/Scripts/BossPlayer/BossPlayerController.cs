using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossPlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput attackInput;
    private SwordAttack swordAttack;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private AudioSource swingSound;
    [SerializeField] private AudioSource hitSound;
    
    private Vector2 _movementInput;
    private Animator _animator;
    private PlayerController _playerController;
    private SpriteRenderer _spriteRenderer;
    private float _currentHealth;
    private Rigidbody2D _rb;
    
    private static readonly int Attack = Animator.StringToHash("SwordAttack");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int IsAlive = Animator.StringToHash("IsAlive");

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        swordAttack = GetComponentInChildren<SwordAttack>();
        
        healthbar.SetMaxHealth((int)maxHealth);
        _currentHealth = maxHealth;
        
        attackInput.OnAttack += OnAttack;
    }

    private void OnAttack(object sender, EventArgs eventArgs)
    {
        _animator.SetTrigger(Attack);
    }
    
    public void SwordAttack()
    {
        swingSound.Play();
        _playerController.DeActivatePlayerInputs();
        if (_spriteRenderer.flipX == true)
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
        _playerController.ActivatePlayerInputs();
    }

    public void OnHit(float damage)
    {
        _animator.SetTrigger(Hit);
        hitSound.Play();
        _currentHealth -= damage;
        healthbar.SetHealth((int)_currentHealth);
        
        if (!(_currentHealth <= 0)) return;
        
        _animator.SetBool(IsAlive, false);
        healthbar.bar.gameObject.SetActive(false);
    }
    
    public void OnKnockBack(Vector2 direction)
    {
        _rb.AddForce(direction);
    }

    public void RemovePlayer()
    {
        Destroy(gameObject);
    }
}
