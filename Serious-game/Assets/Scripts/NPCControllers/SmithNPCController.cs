using System.Collections.Generic;
using UnityEngine;

public class SmithNPCController : NPCController
{
    [SerializeField] private Dialog swordAlreadyAcquiredDialog;
    [SerializeField] private Dialog swordPiecesFoundDialog;
    [SerializeField] private Dialog swordPiecesNotFoundDialog;

    protected override void OnInteract()
    {
        if (PlayerPrefs.GetInt(PlayerPrefKeys.SwordPieces, 0) == 3)
        {
            if (PlayerPrefs.HasKey(PlayerPrefKeys.Sword))
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(swordAlreadyAcquiredDialog));
            }
            else
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(swordPiecesFoundDialog));
                PlayerPrefs.SetInt(PlayerPrefKeys.Sword, 1);
                PlayerPrefs.Save();
            }
        }
        else
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(swordPiecesNotFoundDialog));
        }
    }
}