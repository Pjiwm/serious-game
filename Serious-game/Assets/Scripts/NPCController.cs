using System;
using DefaultNamespace;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialog interactDialog;

    public Action OnDialogFinishedAction { get; set; }

    public void Select()
    {
        Debug.Log("Could you help me?");
    }

    public void Interact()
    {
        Debug.Log(DialogManager.Instance);
        DialogManager.Instance.OnCloseDialog += OnDialogFinished;
        StartCoroutine(DialogManager.Instance.ShowDialog(interactDialog));
    }

    private void OnDialogFinished()
    {
        DialogManager.Instance.OnCloseDialog -= OnDialogFinished;
        OnDialogFinishedAction?.Invoke();
    }
}
