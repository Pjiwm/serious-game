using System;
using DefaultNamespace;
using UnityEngine;

public class LostFriendNPCController : NPCController
{
    [SerializeField] private Dialog interactionDialog;
    private readonly string _name = "maze";

    private void Start()
    {
        if(PlayerPrefs.HasKey(_name)) {
            gameObject.SetActive(false);
        }
    }

    protected override void OnInteract()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(interactionDialog));
        PlayerPrefs.SetInt(_name, 1);
        PlayerPrefs.Save();
    }
    
}
