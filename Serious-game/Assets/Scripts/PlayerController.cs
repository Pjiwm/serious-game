using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
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

        _interactablesLayer = LayerMask.GetMask("Interactables");
    }

    public void ActivatePlayerInputs()
    {
        playerInput.OnInteract += OnInteract;
    }

    public void DeActivatePlayerInputs()
    {
        playerInput.OnInteract -= OnInteract;
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
        foreach (var interactable in _selectedInteractables)
        {
            interactable.Interact();
        }
    }

    private void HandleSelections()
    {
        const float interactDistance = 2f;
        IInteractable[] currentlySelectedInteractables = null;

        var collidedObject = Physics2D.OverlapCircle(_rigidBody2D.position, interactDistance, _interactablesLayer);
        
        if (collidedObject) currentlySelectedInteractables = collidedObject.GetComponents<IInteractable>();
        
        if (currentlySelectedInteractables != _selectedInteractables)
        {
            SetSelectedInteractables(currentlySelectedInteractables);
        }
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
