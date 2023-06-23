using Interactables;
using SceneLoading;
using UnityEngine;

namespace TeleportingAndSpawning
{
    public class BossLevelTeleporter : MonoBehaviour,IInteractable
    {
        [SerializeField] private Dialog.Dialog passDialog;
        [SerializeField] private Dialog.Dialog noPassDialog;


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
            if (GameObject.FindGameObjectsWithTag("Cone").Length != 0) StartCoroutine(DialogManager.Instance.ShowDialog(noPassDialog));
            else
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(passDialog));
                DialogManager.Instance.OnCloseDialog += LoadBossLevel;
            }

        }
        private static void LoadBossLevel()
        {
            DialogManager.Instance.OnCloseDialog -= LoadBossLevel;
            SceneLoader.LoadScene(SceneLoader.Scenes.EndBoss);
        }
    }
}
