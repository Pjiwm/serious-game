using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialog SwordPieceDialog;
    [SerializeField] private GameObject interactionText;
    [SerializeField] private StatsManager _statsManager;
    [SerializeField] private string _name;


    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        if (PlayerPrefs.HasKey(_name))
        {
            _anim.SetBool("IsOpened", true);
        }
    }

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
        if (!PlayerPrefs.HasKey(_name))
        {
            _anim.SetBool("IsOpened", true);
            StartCoroutine(DialogManager.Instance.ShowDialog(SwordPieceDialog));
            int swordPieces = PlayerPrefs.GetInt(StatsManager.SWORDPREF,0);
            swordPieces++;
            StatsManager.updatePref(StatsManager.SWORDPREF,swordPieces);
            _statsManager.requestUpdate();
            PlayerPrefs.SetInt(_name, 1);
            PlayerPrefs.Save();
        }
        else
        {
            var ChestEmptyDialog = new Dialog()
            {
                lines = new List<string> { "Hmmm deze kist is leeg" }
            };
            StartCoroutine(DialogManager.Instance.ShowDialog(ChestEmptyDialog));
        }
    }
}
