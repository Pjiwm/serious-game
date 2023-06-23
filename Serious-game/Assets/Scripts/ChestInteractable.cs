using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialog SwordPieceDialog;
    [FormerlySerializedAs("_statsManager")] [SerializeField] private StatsManager statsManager;
    [SerializeField] private string _name;


    private Animator _anim;
    private static readonly int IsOpened = Animator.StringToHash("IsOpened");

    private void Start()
    {
        _anim = GetComponent<Animator>();
        if(PlayerPrefs.HasKey(_name))
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
        if(!PlayerPrefs.HasKey(_name))
        {
            _anim.SetBool(IsOpened, true);
            
            StartCoroutine(DialogManager.Instance.ShowDialog(SwordPieceDialog));
            
            int swordPieces = PlayerPrefs.GetInt(StatsManager.SWORDPREF,0);
            swordPieces++;
            StatsManager.updatePref(StatsManager.SWORDPREF,swordPieces);
            statsManager.requestUpdate();
            PlayerPrefs.SetInt(_name, 1);
            PlayerPrefs.Save();
        }
        else
        {
            var chestEmptyDialog = new Dialog()
            {
                lines = new List<string> { "Jeff: Hmmm... Deze kist is leeg. Laat ik maar opzoek gaan naar een andere kist." }
            };
            StartCoroutine(DialogManager.Instance.ShowDialog(chestEmptyDialog));
        }
    }
}
