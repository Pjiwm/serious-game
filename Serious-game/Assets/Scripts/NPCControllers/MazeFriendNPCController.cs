using System;
using DefaultNamespace;
using UnityEngine;

public class MazeFriendNPCController : NPCController
{
    [SerializeField] private Dialog interactDialog;
    [SerializeField] private StatsManager _statsManager;
    [SerializeField] private Dialog friendsDialog;


    protected override void OnInteract()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefKeys.Maze))
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(interactDialog));
        }
        else
        {
            if (PlayerPrefs.HasKey(PlayerPrefKeys.MazeFriend))
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(friendsDialog));
            }
            else
            {
                int friends = PlayerPrefs.GetInt(PlayerPrefKeys.Friends, 0);
                friends++;
                StatsManager.UpdatePref(PlayerPrefKeys.Friends, friends);
                _statsManager.RequestUpdate();
                PlayerPrefs.SetInt(PlayerPrefKeys.MazeFriend, 1);
                PlayerPrefs.Save();
                StartCoroutine(DialogManager.Instance.ShowDialog(friendsDialog));
            }
        }
    }
    
}
