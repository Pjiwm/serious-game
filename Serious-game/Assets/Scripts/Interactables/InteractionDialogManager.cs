using UnityEngine;
using UnityEngine.SceneManagement;
namespace Interactables
{
    public class InteractionDialogManager : Singleton<InteractionDialogManager>
    {
        private void Start()
        {
            SceneManager.sceneLoaded += (_, _) =>
            {
                HideInteractionDialog();
            };
        }

        [SerializeField] private GameObject interactionDialogBox;

        public void ShowInteractionDialog()
        {
            interactionDialogBox.SetActive(true);
        }

        public void HideInteractionDialog()
        {
            interactionDialogBox.SetActive(false);
        }
    }
}