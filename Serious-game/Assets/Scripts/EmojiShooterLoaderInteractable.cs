using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmojiShooterLoaderInteractable : MonoBehaviour, IInteractable
{

    public void Select()
    {
        // Debug.Log("Could you help me?");
    }

    public void Deselect()
    {
        //throw new NotImplementedException();
    }

    public void Interact()
    {
        Debug.Log("Loading computer scene");
        SceneLoader.LoadScene(SceneLoader.Scenes.EmojiShooter);
    }
}
