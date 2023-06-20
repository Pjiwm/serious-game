using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmojiShooterLoaderInteractable : MonoBehaviour, IInteractable
{

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
        Debug.Log("Loading computer scene");
        SceneLoader.LoadScene(SceneLoader.Scenes.EmojiShooter);
    }
}
