using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameInput gameInput;
    
    public event EventHandler<SelectedInteractableChangedEventArgs> OnSelectedInteractableChanged;
    
    private Rigidbody2D _rigidBody2D;
    private Vector2 _moveDir;
    private IInteractable _selectedInteractable;
    private float MoveDistance => speed * Time.deltaTime;
    private int _interactablesLayer;

    public class SelectedInteractableChangedEventArgs : EventArgs
    {
        public IInteractable SelectedInteractable { get; set; }
    }
    
    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        gameInput.OnInteract += OnInteract;
        _interactablesLayer = LayerMask.GetMask("Interactables");
    }
    
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        _moveDir = inputVector;
        if (inputVector != Vector2.zero)
        {
            HandleMovement();
        }
        HandleSelections();
    }
    
    private void OnInteract(object sender, EventArgs e)
    {
        _selectedInteractable?.Interact();
    }

    private void HandleSelections()
    {
        const float interactDistance = 2f;
        IInteractable currentlySelectedInteractable = null;

        var collidedObject = Physics2D.OverlapCircle(_rigidBody2D.position, interactDistance, _interactablesLayer);
        
        if (collidedObject) currentlySelectedInteractable = collidedObject.GetComponent<IInteractable>();
        
        if (currentlySelectedInteractable != _selectedInteractable)
        {
            SetSelectedInteractable(currentlySelectedInteractable);
        }
    }
    
    private void SetSelectedInteractable(IInteractable selectedInteractable)
    {
        _selectedInteractable = selectedInteractable;
        _selectedInteractable?.Select();
        OnSelectedInteractableChanged?.Invoke(this, new SelectedInteractableChangedEventArgs { SelectedInteractable = _selectedInteractable });
    }

    private void HandleMovement()
    {
        _rigidBody2D.MovePosition(_rigidBody2D.position + _moveDir * MoveDistance);
    }
}
