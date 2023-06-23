using UnityEngine;

namespace Interactables
{
    public class InteractionDialogManager : Singleton<InteractionDialogManager>
    {
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