using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "ScriptableObjects/PlayerInput", order = 1)]
public class PlayerInput : ScriptableObject
{
    private PlayerInputActions _playerInputActions;
    public event EventHandler OnInteract;
    public event EventHandler OnAttack; 
    private void OnEnable()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        
        _playerInputActions.Player.Attack.performed += OnAttackPerformed;
        _playerInputActions.Player.Interact.performed += OnInteractPerformed;
    }

    private void OnInteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    private void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
    
}
