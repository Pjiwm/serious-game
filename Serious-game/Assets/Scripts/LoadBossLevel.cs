using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBossLevel : MonoBehaviour,IInteractable
{
    public void Select()
    {
        //
    }

    public void Deselect()
    {
        //throw new System.NotImplementedException();
    }

    public void Interact()
    {
        SceneLoader.LoadScene(SceneLoader.Scenes.EndBoss);
    }
}
