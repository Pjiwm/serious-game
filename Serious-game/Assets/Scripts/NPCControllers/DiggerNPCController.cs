using System.Collections.Generic;
using UnityEngine;

namespace NPCControllers
{
    public class DiggerNPCController : NPCController
    {
        [SerializeField] private Dialog.Dialog allowedToPassDialog;
        [SerializeField] private Dialog.Dialog friendsWithoutSwordDialog;
        [SerializeField] private Dialog.Dialog friendsNotMadeDialog;
        [SerializeField] private Dialog.Dialog canAlreadyPassDialog;

        protected override void OnInteract()
        {
            if (PlayerPrefs.GetInt(PlayerPrefKeys.Friends, 0) == 3)
            {
                if(PlayerPrefs.HasKey(PlayerPrefKeys.CanPass))
                {
                    StartCoroutine(DialogManager.Instance.ShowDialog(new Dialog.Dialog() { lines = new List<string> { canAlreadyPassDialog.lines[UnityEngine.Random.Range(0, 3)] } }));
                    DisableCones();
                    return;
                }

                if (PlayerPrefs.HasKey(PlayerPrefKeys.CanPass))
                {
                    StartCoroutine(DialogManager.Instance.ShowDialog(allowedToPassDialog));
                    PlayerPrefs.SetInt(PlayerPrefKeys.CanPass, 1);
                    PlayerPrefs.Save();
                    DisableCones();
                }
                else
                {
                    StartCoroutine(DialogManager.Instance.ShowDialog(friendsWithoutSwordDialog));
                }
            }
            else
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(friendsNotMadeDialog));
            }
        }

        private static void DisableCones()
        {
            var cones = GameObject.FindGameObjectsWithTag("Cone");
            foreach (var cone in cones)
            {
                cone.SetActive(false);
            }
        }
    }
}