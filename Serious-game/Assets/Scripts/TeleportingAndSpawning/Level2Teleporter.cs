using Interactables;
using SceneLoading;
using UnityEngine;

namespace TeleportingAndSpawning
{
    public class Level2Teleporter : MonoBehaviour,IInteractable
    {
        [SerializeField] private Dialog.Dialog internetCafeDialog;


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
            StartCoroutine(DialogManager.Instance.ShowDialog(internetCafeDialog));
            DialogManager.Instance.OnCloseDialog += LoadLevel2;
        }
        private static void LoadLevel2()
        {
            DialogManager.Instance.OnCloseDialog -= LoadLevel2;
            SceneLoader.LoadScene(SceneLoader.Scenes.Level3);
        }
    }
}
