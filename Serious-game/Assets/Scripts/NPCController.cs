using System;
using DefaultNamespace;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialog interactDialog;

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
        StartCoroutine(DialogManager.Instance.ShowDialog(interactDialog));
    }

    private void OnDialogFinished()
    {
        DialogManager.Instance.OnCloseDialog -= OnDialogFinished;
        OnDialogFinishedAction?.Invoke();
    }
}
