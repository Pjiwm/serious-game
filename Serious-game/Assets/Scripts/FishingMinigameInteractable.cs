using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class FishingMinigameInteractable : MonoBehaviour,IInteractable
{
    [SerializeField] private Dialog fishingExplanationDialog;
    public Action OnDialogFinishedAction { get; set; }
    private FishingMinigame _fishingMinigameController;

    private void Start()
    {
        _fishingMinigameController = GetComponent<FishingMinigame>();
    }

    public void Select()
    {
        // throw new System.NotImplementedException();
    }

    public void Interact()
    {
        DialogManager.Instance.OnCloseDialog += OnDialogFinished;
        StartCoroutine(DialogManager.Instance.ShowDialog(fishingExplanationDialog));

        DialogManager.Instance.OnCloseDialog += StartMinigame;
    }

    private void StartMinigame()
    {
        _fishingMinigameController.StartMinigame();
        DialogManager.Instance.OnCloseDialog -= StartMinigame;
    }

    private void OnDialogFinished()
    {
        DialogManager.Instance.OnCloseDialog -= OnDialogFinished;
        OnDialogFinishedAction?.Invoke();
    }
}
