using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    [CreateAssetMenu(fileName = "DialogInput", menuName = "ScriptableObjects/DialogInput", order = 1)]
    public class DialogInput : ScriptableObject
    {
        private PlayerInputActions _playerInputActions;
        public event EventHandler OnSkip;
        private void OnEnable()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Dialog.Enable();
            _playerInputActions.Dialog.Skip.performed += OnSkipPerformed;
        }

        private void OnSkipPerformed(InputAction.CallbackContext obj)
        {
            OnSkip?.Invoke(this, EventArgs.Empty);
        }
    }
}