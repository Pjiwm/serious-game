using System;
using UnityEngine;
using UnityEngine.Serialization;
using Pathfinding;

public class FollowingNPCController : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;
    [SerializeField] private float stopFollowingAtDistance;
    [SerializeField] private Transform target;
    [SerializeField] private float nextWaypointDistance = 0.3f;
    
    private Path _path;
    private int _currentWaypoint = 0;
    private bool _reachedEndOfPath = false;
    private Seeker _seeker;
    private Vector2 _direction;
    
    private NPCController _npcController;
    private MoveController _moveController;
    private Rigidbody2D _rb;

    private bool _isFollowingPlayer;
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsWalkingDown = Animator.StringToHash("isWalkingDown");
    private static readonly int IsWalkingLeft = Animator.StringToHash("isWalkingLeft");
    private static readonly int IsWalkingRight = Animator.StringToHash("isWalkingRight");
    private int _playersLayer;

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _npcController = GetComponent<NPCController>();
        _moveController = GetComponent<MoveController>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();

        _playersLayer = LayerMask.GetMask("Player");

        _animator.SetBool(IsWalking, false);
        _npcController.OnDialogFinishedAction += () => { _isFollowingPlayer = true; };
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    private void Update()
    {
        if (_isFollowingPlayer)
        {
            FollowPlayer();
        }
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

    private void FollowPlayer()
    {
        if (_path == null)
        {
            return;
        }
        
        if (_currentWaypoint >= _path.vectorPath.Count - 1)
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
        if (!_animator.GetBool(IsWalking)) _animator.SetBool(IsWalking, true);
        
        _rb.AddForce(force);
        
        if (!_animator.GetBool(IsWalking)) _animator.SetBool(IsWalking, true);
        HandleAnimation(_direction.normalized);
    }

    private void HandleAnimation(Vector2 inputVector)
    {
        if (inputVector == Vector2.zero)
        {
            _animator.SetBool(IsWalking, false);
            _animator.SetBool(IsWalkingRight, false);
            _animator.SetBool(IsWalkingLeft, false);
            _animator.SetBool(IsWalkingDown, false);
            return;
        }

        _animator.SetBool(IsWalking, true);

        var walkRight = false;
        var walkLeft = false;
        var walkDown = false;

        if (inputVector.x > 0 && Math.Abs(inputVector.x) > Math.Abs(inputVector.y)) walkRight = true;
        if (inputVector.x < 0 && Math.Abs(inputVector.x) > Math.Abs(inputVector.y)) walkLeft = true;
        if (inputVector.y < 0 && Math.Abs(inputVector.y) > Math.Abs(inputVector.x)) walkDown = true;
        
        _animator.SetBool(IsWalkingRight, walkRight);
        _animator.SetBool(IsWalkingLeft, walkLeft);
        _animator.SetBool(IsWalkingDown, walkDown);
    }
}