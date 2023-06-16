using System;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowingNPCController : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;
    [SerializeField] private float interactDistance;
    private NPCController _npcController;
    private MoveController _moveController;
    private Rigidbody2D _rb;

    private bool _isFollowingPlayer;
    private Animator animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsWalkingDown = Animator.StringToHash("isWalkingDown");
    private static readonly int IsWalkingLeft = Animator.StringToHash("isWalkingLeft");
    private static readonly int IsWalkingRight = Animator.StringToHash("isWalkingRight");
    private int _playersLayer;

    private void Start()
    {
        _npcController = GetComponent<NPCController>();
        _moveController = GetComponent<MoveController>();
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();

        _playersLayer = LayerMask.GetMask("Player");

        animator.SetBool(IsWalking, false);
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
        var collidedObject = Physics2D.OverlapCircle(_rb.position, interactDistance, _playersLayer);

        if (collidedObject)
        {
            animator.SetBool(IsWalking, false);
            return;
        }

        if (!animator.GetBool(IsWalking)) animator.SetBool(IsWalking, true);

        var nextToPlayerPosition = playerLocation.position;

        var normalizedDirection = nextToPlayerPosition - _npcController.transform.position;

        HandleAnimation(normalizedDirection);
        _moveController.HandleMovement(normalizedDirection);
    }

    private void HandleAnimation(Vector2 inputVector)
    {
        if (inputVector == Vector2.zero)
        {
            animator.SetBool(IsWalking, false);
            animator.SetBool(IsWalkingRight, false);
            animator.SetBool(IsWalkingLeft, false);
            animator.SetBool(IsWalkingDown, false);
            return;
        }

        animator.SetBool(IsWalking, true);

        var walkRight = false;
        var walkLeft = false;
        var walkDown = false;

        if (inputVector.x > 0 && Math.Abs(inputVector.x) > Math.Abs(inputVector.y)) walkRight = true;
        if (inputVector.x < 0 && Math.Abs(inputVector.x) > Math.Abs(inputVector.y)) walkLeft = true;
        if (inputVector.y < 0 && Math.Abs(inputVector.y) > Math.Abs(inputVector.x)) walkDown = true;
        
        animator.SetBool(IsWalkingRight, walkRight);
        animator.SetBool(IsWalkingLeft, walkLeft);
        animator.SetBool(IsWalkingDown, walkDown);
    }
}