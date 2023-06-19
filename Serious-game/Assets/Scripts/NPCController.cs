using System;
using DefaultNamespace;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialog interactDialog;
    [SerializeField] private GameObject interactionText;

    public Action OnDialogFinishedAction { get; set; }

    public void Select()
    {
        interactionText.SetActive(true);
    }

    public void Deselect()
    {
        interactionText.SetActive(false);
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
