using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using Pathfinding;

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
    [SerializeField] private Transform target;
    [SerializeField] private float nextWaypointDistance = 3f;
    
    private Path _path;
    private int _currentWaypoint = 0;
    private bool _reachedEndOfPath = false;
    private Seeker _seeker;
    private Vector2 _direction;
    
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
        _seeker = GetComponentInChildren<Seeker>();
        _health = maxHealth;
        healthbar.SetMaxHealth((int)maxHealth);
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _moveController = GetComponent<MoveController>();
        _playersLayer = LayerMask.GetMask("Player");
        
        InvokeRepeating("UpdatePath", 0f, .5f);
    }
    
    private void UpdatePath()
    {
        if (target != null)
        {
            if (_seeker.IsDone())
            {
                _seeker.StartPath(_rb.position, target.position, OnPathComplete);
            }
        }
        
    }
    
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

    private void Update()
    {
        if (_path == null)
        {
            return;
        }
        
        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            _reachedEndOfPath = true;
            return;
        }
        else
        {
            _reachedEndOfPath = false;
        }

        Vector2 tempDir = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        if (tempDir != Vector2.zero)
        {
            _direction = tempDir;
        }
        
        Vector2 force = _direction * 300f * Time.deltaTime;
        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
        
        if (distance < nextWaypointDistance)
        {
            _currentWaypoint++;
        }

        if (_canMove)
        {
            if (!_animator.GetBool(IsMoving)) _animator.SetBool(IsMoving, true);
            //_moveController.HandleMovement(_direction.normalized);
            _rb.AddForce(force);
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
            player.OnHit(6);
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


