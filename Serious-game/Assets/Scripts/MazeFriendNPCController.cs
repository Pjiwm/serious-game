using System;
using DefaultNamespace;
using UnityEngine;

public class MazeFriendNPCController : NPCController
{
    [SerializeField] private Dialog interactDialog;
    [SerializeField] private StatsManager _statsManager;
    [SerializeField] private Dialog friendsDialog;
    private readonly string _name = "maze";
    private readonly string _pref = "mazeFriend";

    
    protected override void OnInteract()
    {
        if (!PlayerPrefs.HasKey(_name))
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(interactDialog));
        }
        else
        {
            if (PlayerPrefs.HasKey(_pref))
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(friendsDialog));
            }
            else
            {
                int friends = PlayerPrefs.GetInt(StatsManager.FRIENDPREF, 0);
                friends++;
                StatsManager.updatePref(StatsManager.FRIENDPREF, friends);
                _statsManager.requestUpdate();
                PlayerPrefs.SetInt(_pref, 1);
                PlayerPrefs.Save();
                StartCoroutine(DialogManager.Instance.ShowDialog(friendsDialog));
            }
        }
    }
    
}
