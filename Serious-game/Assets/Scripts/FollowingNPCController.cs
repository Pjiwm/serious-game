using UnityEngine;

public class FollowingNPCController : MonoBehaviour
{
    [SerializeField] private Transform _playerLocation;
    
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
        _npcController.OnDialogFinishedAction += () =>
        {
            _isFollowingPlayer = true;
        };
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
        const float interactDistance = 3f;

        var collidedObject = Physics2D.OverlapCircle(_rb.position, interactDistance, _playersLayer);

        if (collidedObject)
        {
            animator.SetBool(IsWalking, false);
            return;
        }
        if (!animator.GetBool(IsWalking)) animator.SetBool(IsWalking, true);
        
        var nextToPlayerPosition = _playerLocation.position;

        var direction = nextToPlayerPosition - _npcController.transform.position;
        Debug.Log(direction.normalized);
        _moveController.HandleMovement(direction.normalized);
        HandleAnimation(direction.normalized);
    }

    private void HandleAnimation(Vector2 inputVector)
    {
        if (inputVector != Vector2.zero)
        {
            animator.SetBool(IsWalking, false);
            animator.SetBool(IsWalkingRight, false);
            animator.SetBool(IsWalkingLeft, false);
            animator.SetBool(IsWalkingDown, false);
        }
        else
        {
            animator.SetBool(IsWalking, true);
            if (inputVector.x > 0)
            {
                animator.SetBool(IsWalkingRight, true);
                animator.SetBool(IsWalkingLeft, false);
                animator.SetBool(IsWalkingDown, false);
            }
            if (inputVector.x < 0)
            {
                animator.SetBool(IsWalkingRight, false);
                animator.SetBool(IsWalkingLeft, true);
                animator.SetBool(IsWalkingDown, false);
            }
            if (inputVector.y < 0)
            {
                animator.SetBool(IsWalkingRight, false);
                animator.SetBool(IsWalkingLeft, false);
                animator.SetBool(IsWalkingDown, true);
            }
        }
    }
}