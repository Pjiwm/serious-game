using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class EndBossTeleporter : MonoBehaviour
{
    [SerializeField] private Dialog EndbossTeleporterDialog;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(EndbossTeleporterDialog));
        DialogManager.Instance.OnCloseDialog += LoadLevel;
    }
    private void LoadLevel()
    {
        DialogManager.Instance.OnCloseDialog -= LoadLevel;
        SceneLoader.LoadScene(SceneLoader.Scenes.EndBoss);
    }
}
