using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactables
{
    public class ChestInteractable : MonoBehaviour, IInteractable
    {
        [FormerlySerializedAs("SwordPieceDialog")][SerializeField] private Dialog.Dialog swordPieceDialog;
        [FormerlySerializedAs("_statsManager")][SerializeField] private StatsManager statsManager;
        [SerializeField] private string _name;


        private Animator _anim;

        private static readonly int IsOpened = Animator.StringToHash("IsOpened");

        private void Start()
        {
            _anim = GetComponent<Animator>();
            if (PlayerPrefs.HasKey(_name))
            {
                _anim.SetBool(IsOpened, true);
            }
        }

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
            if (!PlayerPrefs.HasKey(_name))
            {
                _anim.SetBool(IsOpened, true);
                var audioSource = GetComponent<AudioSource>();
                audioSource.Play();
                StartCoroutine(DialogManager.Instance.ShowDialog(swordPieceDialog));
                var swordPieces = PlayerPrefs.GetInt(PlayerPrefKeys.SwordPieces, 0);
                swordPieces++;
                StatsManager.UpdatePref(PlayerPrefKeys.SwordPieces, swordPieces);
                statsManager.RequestUpdate();
                PlayerPrefs.SetInt(_name, 1);
                PlayerPrefs.Save();
            }
            else
            {
                var ChestEmptyDialog = new Dialog.Dialog()
                {
                    lines = new List<string> { "Jeff: Hmmm... Deze kist is leeg. Laat ik maar opzoek gaan naar een andere kist." }
                };
                StartCoroutine(DialogManager.Instance.ShowDialog(ChestEmptyDialog));
            }
        }
    }
}
