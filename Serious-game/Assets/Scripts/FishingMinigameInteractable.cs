using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class FishingMinigameInteractable : MonoBehaviour,IInteractable
{
    [SerializeField] private Dialog fishingExplanationDialog;
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
        StartCoroutine(DialogManager.Instance.ShowDialog(fishingExplanationDialog));
        DialogManager.Instance.OnCloseDialog += StartMiniGameCoroutine;
    }
    private void StartMiniGameCoroutine()
    {
        StartCoroutine(StartMinigame());
    }
    
    private IEnumerator StartMinigame()
    {
        DialogManager.Instance.OnCloseDialog -= StartMiniGameCoroutine;
        yield return new WaitForSeconds(0.5f);
        _fishingMinigameController.StartMinigame();
        
    }
}
