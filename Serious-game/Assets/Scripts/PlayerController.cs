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
    private Vector2 _lastInteractDir;
    private Vector2 _moveDir;
    private IInteractable _selectedInteractable;
    private float MoveDistance => speed * Time.deltaTime;

    public class SelectedInteractableChangedEventArgs : EventArgs
    {
        public IInteractable SelectedInteractable { get; set; }
    }
    
    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        gameInput.OnInteract += OnInteract;
    }
    
    void Update()
    {
        
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        _moveDir = inputVector;
        if (inputVector != Vector2.zero)
        {
            _lastInteractDir = _moveDir;
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
        var interactDistance = 2f;
        IInteractable currentlySelectedCounter = null;
        var raycanon =
            Physics2D.Raycast(transform.position,
                _lastInteractDir, interactDistance);
        if(raycanon)
        {
            if (raycanon.transform.TryGetComponent(out IInteractable interactable))
            {
                currentlySelectedCounter = interactable;
            }
        }

        if (currentlySelectedCounter != _selectedInteractable)
        {
            SetSelectedInteractable(currentlySelectedCounter);
        }
    }
    
    private void SetSelectedInteractable(IInteractable selectedInteractable)
    {
        _selectedInteractable = selectedInteractable;
        OnSelectedInteractableChanged?.Invoke(this, new SelectedInteractableChangedEventArgs { SelectedInteractable = _selectedInteractable });
    }

    private void HandleMovement()
    {
        _rigidBody2D.MovePosition(_rigidBody2D.position + _moveDir * MoveDistance);
    }
}
