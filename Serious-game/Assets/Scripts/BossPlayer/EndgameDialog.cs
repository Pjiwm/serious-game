using System;
using SceneLoading;
using UnityEngine;

public class EndgameDialog : MonoBehaviour
{
    [SerializeField] private Dialog.Dialog dialog;
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
        DialogManager.Instance.OnCloseDialog += LoadMainMenu;
    }
    
    private void LoadMainMenu()
    {
        DialogManager.Instance.OnCloseDialog -= LoadMainMenu;
        SceneLoader.LoadScene(SceneLoader.Scenes.StartMenu);
    }
}