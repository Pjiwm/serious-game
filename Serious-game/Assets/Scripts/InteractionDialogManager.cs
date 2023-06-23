using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionDialogManager : Singleton<InteractionDialogManager>
{
    [SerializeField] private GameObject interactionDialogBox;

    private void Start()
    {
        SceneManager.sceneLoaded += (arg0, mode) =>
        {
            HideInteractionDialog();
        };
    }

    public void ShowInteractionDialog()
    {
        interactionDialogBox.SetActive(true);
    }

    public void HideInteractionDialog()
    {
        interactionDialogBox.SetActive(false);
    }
}