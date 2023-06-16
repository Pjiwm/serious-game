using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "DialogueInput", menuName = "ScriptableObjects/DialogueInput", order = 1)]
public class DialogueInput : ScriptableObject
{
    private PlayerInputActions _playerInputActions;
    public event EventHandler OnSkip;
    private void OnEnable()
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