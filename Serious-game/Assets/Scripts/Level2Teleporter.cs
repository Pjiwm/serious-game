using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class Level2Teleporter : MonoBehaviour,IInteractable
{
    [SerializeField] private Dialog internetCafeDialog;
    [SerializeField] private GameObject interactionText;


    public void Select()
    {
        interactionText.SetActive(true);
    }

    public void Deselect()
    {
        interactionText.SetActive(false);
    }

    public void Interact()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(internetCafeDialog));
        DialogManager.Instance.OnCloseDialog += LoadLevel2;
    }
    private void LoadLevel2()
    {
        DialogManager.Instance.OnCloseDialog -= LoadLevel2;
        SceneLoader.LoadScene(SceneLoader.Scenes.Level3);
    }
}
