using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderController : MonoBehaviour, IInteractable
{
    public string sceneName;

    public void Select()
    {
        // Debug.Log("Could you help me?");
    }

    public void Interact()
    {
        Debug.Log("Loading computer scene");
        SceneManager.LoadScene(sceneName);
    }
}
