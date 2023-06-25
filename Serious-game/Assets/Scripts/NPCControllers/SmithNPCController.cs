using UnityEngine;

namespace NPCControllers
{
    public class SmithNPCController : NPCController
    {
        [SerializeField] private Dialog.Dialog swordAlreadyAcquiredDialog;
        [SerializeField] private Dialog.Dialog swordPiecesFoundDialog;
        [SerializeField] private Dialog.Dialog swordPiecesNotFoundDialog;

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
}