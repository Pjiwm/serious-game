using System;
using DefaultNamespace;
using UnityEngine;

public class LostFriendNPCController : NPCController
{
    [SerializeField] private Dialog interactionDialog;

    private void Start()
    {
        if(PlayerPrefs.HasKey(PlayerPrefKeys.Maze)) {
            gameObject.SetActive(false);
        }
    }

    protected override void OnInteract()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(interactionDialog));
        PlayerPrefs.SetInt(PlayerPrefKeys.Maze, 1);
        PlayerPrefs.Save();
    }
    
}
