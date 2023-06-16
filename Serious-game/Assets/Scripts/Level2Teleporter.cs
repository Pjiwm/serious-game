using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class Level2Teleporter : MonoBehaviour,IInteractable
{
    [SerializeField] private Dialog internetCafeDialog;

    public void Select()
    {
        // throw new System.NotImplementedException();
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
