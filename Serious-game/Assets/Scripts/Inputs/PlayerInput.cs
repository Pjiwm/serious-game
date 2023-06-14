using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "ScriptableObjects/PlayerInput", order = 1)]
public class PlayerInput : ScriptableObject
{
    private PlayerInputActions _playerInputActions;
    public event EventHandler OnInteract;
    public event EventHandler OnAttack;
    public event EventHandler OnFishing;
    public event EventHandler OnFishingStarted;
    public event EventHandler OnFishingCanceled;
    private void OnEnable()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        
        _playerInputActions.Player.Attack.performed += OnAttackPerformed;
        _playerInputActions.Player.Interact.performed += OnInteractPerformed;
        _playerInputActions.Player.Fishing.performed += OnFishingPerformed;
        
        _playerInputActions.Player.Fishing.started += OnFishingStartedPerformed;
        _playerInputActions.Player.Fishing.canceled += OnFishingCanceledPerformed;
    }

    private void OnFishingStartedPerformed(InputAction.CallbackContext obj)
    {
        OnFishingStarted?.Invoke(this, EventArgs.Empty);
    }

    private void OnFishingCanceledPerformed(InputAction.CallbackContext obj)
    {
        OnFishingCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void OnFishingPerformed(InputAction.CallbackContext obj)
    {
        OnFishing?.Invoke(this, EventArgs.Empty);
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
