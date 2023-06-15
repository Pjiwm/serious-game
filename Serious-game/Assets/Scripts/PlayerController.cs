using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float interactDistance;
    private SpriteRenderer _spriteRenderer;
    private MoveController _moveController;
    private Rigidbody2D _rigidBody2D;
    private Vector2 _moveDir;
    private IInteractable[] _selectedInteractables;
    private int _interactablesLayer;
    private Animator _animator;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _moveController = GetComponent<MoveController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        
        playerInput.OnInteract += OnInteract;
        GameStateController.Instance.OnRoaming += ActivatePlayerInputs;
        GameStateController.Instance.OnInDialogue += DeActivatePlayerInputs;
        GameStateController.Instance.OnMinigame += DeActivatePlayerInputs;
        _interactablesLayer = LayerMask.GetMask("Interactables");
    }

    public void ActivatePlayerInputs()
    {
        playerInput.OnInteract += OnInteract;
    }

    public void DeActivatePlayerInputs()
    {
        playerInput.OnInteract -= OnInteract;
        _animator.SetBool(IsMoving, false);
    }

    public void HandleUpdate()
    {
        var inputVector = playerInput.GetMovementVectorNormalized();
        if (inputVector != Vector2.zero)
        {
            _moveDir = inputVector;
            if (inputVector.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (inputVector.x > 0)
            {
                _spriteRenderer.flipX = false;
            }

            _animator.SetBool(IsMoving, true);
            _moveController.HandleMovement(_moveDir);
        } else {
            _animator.SetBool(IsMoving, false);
        }
        

        HandleSelections();
    }

    private void OnInteract(object sender, EventArgs e)
    {
        if (_selectedInteractables == null) return;
        foreach (var interactable in _selectedInteractables)
        {
            interactable.Interact();
        }
    }

    private void HandleSelections()
    {
        IInteractable[] currentlySelectedInteractables = null;


        var collidedObjects = Physics2D.OverlapCircleAll(_rigidBody2D.position, interactDistance, _interactablesLayer);
        if (collidedObjects.Length != 0)
        {
            //https://stackoverflow.com/questions/914109/how-to-use-linq-to-select-object-with-minimum-or-maximum-property-value
            Collider2D closestObject;
            if (collidedObjects.Length == 1)
            {
                closestObject = collidedObjects.First();
            }
            else
            {
                closestObject = 
                    collidedObjects.Select(
                    ob => (GetDistanceToPlayer(gameObject.transform.position, ob.transform.position), ob)
                    ).Min().Item2;
            }
            currentlySelectedInteractables = closestObject.GetComponents<IInteractable>();
        }
        
        if (currentlySelectedInteractables != _selectedInteractables)
        {
            SetSelectedInteractables(currentlySelectedInteractables);
        }
    }
    private float GetDistanceToPlayer(Vector2 playerPosition, Vector2 objectPosition)
    {
        return Math.Abs((playerPosition - objectPosition).magnitude);
    }
    
    private void SetSelectedInteractables(IInteractable[] selectedInteractables)
    {
        _selectedInteractables = selectedInteractables;
        
        if (selectedInteractables == null) return;
        
        foreach (var interactable in _selectedInteractables)
        {
            interactable?.Select();
        } 
    }

    private void OnDestroy()
    {
        playerInput.OnInteract -= OnInteract;
    }
}
