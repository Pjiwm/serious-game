using System.Collections.Generic;
using UnityEngine;

public class SmithNPCController : NPCController
{
    [SerializeField] private StatsManager _statsManager;
    [SerializeField] private readonly string _name = "sword";
    [SerializeField] private Dialog swordAlreadyAcquiredDialog;
    [SerializeField] private Dialog swordPiecesFoundDialog;
    [SerializeField] private Dialog swordPiecesNotFoundDialog;

    protected override void OnInteract()
    {
        if (PlayerPrefs.GetInt(StatsManager.SWORDPREF, 0) == 3)
        {
            if (PlayerPrefs.HasKey(_name))
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(swordAlreadyAcquiredDialog));
            }
            else
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(swordPiecesFoundDialog));
                PlayerPrefs.SetInt(_name, 1);
                PlayerPrefs.Save();
            }
        }
        else
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(swordPiecesNotFoundDialog));
        }
    }
}