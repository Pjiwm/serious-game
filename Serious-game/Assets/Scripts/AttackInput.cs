using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    public event EventHandler OnAttack;
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Attack.performed += OnAttackPerformed;
    }

    private void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke(this, EventArgs.Empty);
    }
}
