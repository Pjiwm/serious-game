using System;
using DefaultNamespace;
using UnityEngine;

public class FishingNPCController : NPCController
{
    [SerializeField] private Dialog interactDialog;
    private readonly string _pref = "fishingRod";
    [SerializeField] private StatsManager _statsManager;
    [SerializeField] private Dialog friendsDialog;
    private readonly string _name = "fish";
    
    protected override void OnInteract()
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
    
}
