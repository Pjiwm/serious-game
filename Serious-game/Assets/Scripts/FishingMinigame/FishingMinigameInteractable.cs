using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

namespace FishingMinigame
{
    public class FishingMinigameInteractable : MonoBehaviour,IInteractable
    {
        [SerializeField] private Dialog.Dialog fishingExplanationDialog;
        [SerializeField] private GameObject interactionText;
        [SerializeField] private string _name;
        private FishingMinigameController _fishingMinigameController;

        private void Start()
        {
            if(PlayerPrefs.HasKey(_name))
            {
                gameObject.SetActive(false);
            }
            _fishingMinigameController = GetComponent<FishingMinigameController>();
        }

        public void Select()
        {
            InteractionDialogManager.Instance.ShowInteractionDialog();
        }

        public void Deselect()
        {
            InteractionDialogManager.Instance.HideInteractionDialog();
        }

        //write a wait for seconds here method here

    

        public void Interact()
        {
            if (PlayerPrefs.HasKey("fishingRod"))
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(fishingExplanationDialog));
                DialogManager.Instance.OnCloseDialog += StartMiniGameCoroutine;
            }
            else
            {
                var noFishingRotDialog = new Dialog.Dialog()
                {
                    lines = new List<string> { "Je kan niet vissen, want je hebt geen hengel!" }
                };
                StartCoroutine(DialogManager.Instance.ShowDialog(noFishingRotDialog));
            }
        }
        private void StartMiniGameCoroutine()
        {
            StartCoroutine(StartMinigame());
        }
    
        private IEnumerator StartMinigame()
        {
            DialogManager.Instance.OnCloseDialog -= StartMiniGameCoroutine;
            yield return new WaitForSeconds(0.5f);
            _fishingMinigameController.StartMinigame();
        }
    }
}
