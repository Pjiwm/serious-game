using Interactables;
using SceneLoading;
using UnityEngine;
using PlayerAndMovement;

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
            PlayerPrefs.SetFloat(PlayerPositionPrefs.X, 0.17f);
            PlayerPrefs.SetFloat(PlayerPositionPrefs.Y, -0.87f);
            DialogManager.Instance.OnCloseDialog -= LoadLevel2;
            SceneLoader.LoadScene(SceneLoader.Scenes.Level3);
        }
    }
}
