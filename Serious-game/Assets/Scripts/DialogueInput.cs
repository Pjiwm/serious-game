using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    public event EventHandler OnSkip;
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Dialogue.Enable();
        _playerInputActions.Dialogue.Skip.performed += OnSkipPerformed;
    }

    private void OnSkipPerformed(InputAction.CallbackContext obj)
    {
        OnSkip?.Invoke(this, EventArgs.Empty);
    }
}