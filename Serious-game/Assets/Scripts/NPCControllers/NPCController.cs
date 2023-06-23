using System;
using Interactables;
using UnityEngine;

namespace NPCControllers
{
    public abstract class NPCController : MonoBehaviour, IInteractable
    {
        public Action OnDialogFinishedAction { get; set; }

        public void Select()
        {
            InteractionDialogManager.Instance.ShowInteractionDialog();
        }

        public void Deselect()
        {
            InteractionDialogManager.Instance.HideInteractionDialog();
        }

        public void Interact()
        {
            DialogManager.Instance.OnCloseDialog += OnDialogFinished;
            OnInteract();
        }
    
        protected abstract void OnInteract();

        private void OnDialogFinished()
        {
            DialogManager.Instance.OnCloseDialog -= OnDialogFinished;
            OnDialogFinishedAction?.Invoke();
        }
    }
}
