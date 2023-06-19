using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float _health;

    public float Health
    {
        get => _health;
        set
        {
            if (_health <= 0f) return;

            _health = value;
            healthbar.SetHealth((int)_health);
            if (_health <= 0f)
            {
                Defeated();
            }
        }
    }

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private Dialog winDialog;
    [SerializeField] private float knockBackForce = 3f;
    [SerializeField] private AudioSource hitSound;
    private Animator _animator;
    private Rigidbody2D _rb;
    private MoveController _moveController;
    private bool _canMove = true;
    private int _playersLayer;
    
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsAlive = Animator.StringToHash("IsAlive");
    private static readonly int Hit = Animator.StringToHash("Hit");

    private void Start()
    {
        _health = maxHealth;
        healthbar.SetMaxHealth((int)maxHealth);
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _moveController = GetComponent<MoveController>();
        _playersLayer = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        const float interactDistance = 10f;
        
        var collidedObject = Physics2D.OverlapCircle(_rb.position, interactDistance, _playersLayer);
        if (!collidedObject) return;
        
        var nextToPlayerPosition = collidedObject.gameObject.transform.position;
        var direction = nextToPlayerPosition - gameObject.transform.position;
        var nextToPlayerDistance = 0.15;
        
        if (Math.Abs(direction.x) <= nextToPlayerDistance && Math.Abs(direction.y) <= nextToPlayerDistance) return;

        if (collidedObject && _canMove)
        {
            if (!_animator.GetBool(IsMoving)) _animator.SetBool(IsMoving, true);
        
            
            
            _moveController.HandleMovement(direction.normalized);
            return;
        }
        _animator.SetBool(IsMoving, false);
    }

    public void Defeated()
    {
        _animator.SetBool(IsAlive, false);
        healthbar.bar.gameObject.SetActive(false);
        StartCoroutine(DialogManager.Instance.ShowDialog(winDialog));
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void OnHit(float damage)
    {
        LockMovement();
        _animator.SetTrigger(Hit);
        hitSound.Play();
        Health -= damage;
    }

    public void OnKnockBack(Vector2 direction)
    {
        _rb.AddForce(direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BossPlayerController player = other.gameObject.GetComponent<BossPlayerController>();
        if (player != null)
        {
            player.OnHit(10);
            player.OnKnockBack(KnockBack(other));
        }
    }
    
    public void LockMovement()
    {
        _canMove = false;
    }

    public void UnlockMovement()
    {
        _canMove = true;
    }
    
    private Vector2 KnockBack(Collider2D other)
    {
        var positionPlayer = gameObject.GetComponentInParent<Transform>().position;
        var direction = (Vector2)(other.gameObject.transform.position - positionPlayer).normalized;
        var knockback = direction * knockBackForce;
        return knockback;
    }
}


