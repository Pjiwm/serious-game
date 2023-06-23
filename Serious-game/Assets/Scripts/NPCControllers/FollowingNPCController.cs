using System;
using UnityEngine;
using UnityEngine.Serialization;
using Pathfinding;

public class FollowingNPCController : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;
    [SerializeField] private float stopFollowingAtDistance;
    [SerializeField] private float nextWaypointDistance = 0.3f;
    
    private Path _path;
    private int _currentWaypoint;
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

    private void Start()
    {
        _seeker = GetComponentInChildren<Seeker>();
        _npcController = GetComponent<NPCController>();
        _moveController = GetComponent<MoveController>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();

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
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, playerLocation.position, OnPathComplete);
        }
    }
    
    private void OnPathComplete(Path p)
    {
        if (p.error) return;
        
        _path = p;
        _currentWaypoint = 0;
    }

    private void FollowPlayer()
    {
        if (_path == null) return;

        if (_currentWaypoint > _path.vectorPath.Count - 5 && _currentWaypoint == 0)
        {
            _animator.SetBool(IsWalking, false);
            return;
        }

        _direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        
        var distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
        
        if (distance > stopFollowingAtDistance) return;

        if (distance < nextWaypointDistance) _currentWaypoint++;


        if (!_animator.GetBool(IsWalking)) _animator.SetBool(IsWalking, true);
        
        HandleAnimation(_direction);
        
        _moveController.HandleMovement(_direction);
    }

    private void HandleAnimation(Vector2 direction)
    {
        if (direction == Vector2.zero)
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

        if (direction.x > 0 && Math.Abs(direction.x) > Math.Abs(direction.y)) walkRight = true;
        if (direction.x < 0 && Math.Abs(direction.x) > Math.Abs(direction.y)) walkLeft = true;
        if (direction.y < 0 && Math.Abs(direction.y) > Math.Abs(direction.x)) walkDown = true;
        
        _animator.SetBool(IsWalkingRight, walkRight);
        _animator.SetBool(IsWalkingLeft, walkLeft);
        _animator.SetBool(IsWalkingDown, walkDown);
    }
}