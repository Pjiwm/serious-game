using System;
using DefaultNamespace;
using UnityEngine;

public class NPCControllerPrefs : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialog interactDialog;
    [SerializeField] private string _pref;
    [SerializeField] private StatsManager _statsManager;
    [SerializeField] private Dialog friendsDialog;
    [SerializeField] private string _name;




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
        if (!PlayerPrefs.HasKey(_name))
        {
            if (GameObject.FindObjectsOfType(typeof(FishingMinigameInteractable)).Length == 0)
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(friendsDialog));
                int friends = PlayerPrefs.GetInt(StatsManager.FRIENDPREF, 0);
                friends++;
                StatsManager.updatePref(StatsManager.FRIENDPREF, friends);
                _statsManager.requestUpdate();
                PlayerPrefs.SetInt(_name, 1);
                PlayerPrefs.Save();
            }
            else
            {
                DialogManager.Instance.OnCloseDialog += OnDialogFinished;
                StartCoroutine(DialogManager.Instance.ShowDialog(interactDialog));
                PlayerPrefs.SetInt(_pref, 1);
                PlayerPrefs.Save();
            }
        }
        else
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(friendsDialog));
        }
    }

    private void OnDialogFinished()
    {
        DialogManager.Instance.OnCloseDialog -= OnDialogFinished;
        OnDialogFinishedAction?.Invoke();
    }
}
