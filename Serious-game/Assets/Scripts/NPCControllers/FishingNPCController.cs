using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class FishingNPCController : NPCController
{
    [SerializeField] private Dialog interactDialog;
    [FormerlySerializedAs("_statsManager")] [SerializeField] private StatsManager statsManager;
    [SerializeField] private Dialog friendsDialog;

    protected override void OnInteract()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefKeys.Fish))
        {
            if (GameObject.FindObjectsOfType(typeof(FishingMinigameInteractable)).Length == 0)
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(friendsDialog));
                var friends = PlayerPrefs.GetInt(PlayerPrefKeys.Friends, 0);
                friends++;
                StatsManager.UpdatePref(PlayerPrefKeys.Friends, friends);
                statsManager.RequestUpdate();
                PlayerPrefs.SetInt(PlayerPrefKeys.Fish, 1);
                PlayerPrefs.Save();
            }
            else
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(interactDialog));
                PlayerPrefs.SetInt(PlayerPrefKeys.FishingRod, 1);
                PlayerPrefs.Save();
            }
        }
        else
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(friendsDialog));
        }
    }
    
}
