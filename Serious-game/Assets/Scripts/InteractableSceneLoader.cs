using DefaultNamespace;
using UnityEngine;

public class InteractableSceneLoader : MonoBehaviour, IInteractable
{
        
    [SerializeField] private SceneLoader.Scenes sceneToLoad;
    
    public void Select()
    {
        InteractionDialogManager.Instance.ShowInteractionDialog();
    }

    public void Deselect()
    {
        InteractionDialogManager.Instance.HideInteractionDialog();
    }

    public void Interact()
    {
        SceneLoader.LoadScene(sceneToLoad);
    }
}