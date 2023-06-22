using System;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowingNPCController : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;
    [SerializeField] private float stopFollowingAtDistance;
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
        _npcController = GetComponent<NPCController>();
        _moveController = GetComponent<MoveController>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();

        _playersLayer = LayerMask.GetMask("Player");

        _animator.SetBool(IsWalking, false);
        _npcController.OnDialogFinishedAction += () => { _isFollowingPlayer = true; };
    }

    private void Update()
    {
        if (_isFollowingPlayer)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        var collidedObject = Physics2D.OverlapCircle(_rb.position, stopFollowingAtDistance, _playersLayer);

        if (collidedObject)
        {
            _animator.SetBool(IsWalking, false);
            return;
        }

        if (!_animator.GetBool(IsWalking)) _animator.SetBool(IsWalking, true);

        var nextToPlayerPosition = playerLocation.position;

        var normalizedDirection = nextToPlayerPosition - gameObject.transform.position;

        HandleAnimation(normalizedDirection);
        _moveController.HandleMovement(normalizedDirection);
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