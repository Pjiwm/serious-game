using UnityEngine;
using UnityEngine.Serialization;

namespace NPCControllers
{
    public class MazeFriendNPCController : NPCController
    {
        [FormerlySerializedAs("_statsManager")][SerializeField] private StatsManager statsManager;
        [SerializeField] private Dialog.Dialog interactDialog;
        [SerializeField] private Dialog.Dialog friendsDialog;
        [SerializeField] private Dialog.Dialog alreadySpokenToDialog;


        protected override void OnInteract()
        {
            if (PlayerPrefs.HasKey(PlayerPrefKeys.MazeFriendMade))
            {
                var friends = PlayerPrefs.GetInt(PlayerPrefKeys.Friends, 0);
                friends++;
                StatsManager.UpdatePref(PlayerPrefKeys.Friends, friends);
                statsManager.RequestUpdate();

                StartCoroutine(DialogManager.Instance.ShowDialog(friendsDialog));
            }
            else if (PlayerPrefs.HasKey(PlayerPrefKeys.MazeFriendSpokenTo))
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(alreadySpokenToDialog));
            }
            else
            {
                PlayerPrefs.SetInt(PlayerPrefKeys.MazeFriendSpokenTo, 1);
                PlayerPrefs.Save();
                StartCoroutine(DialogManager.Instance.ShowDialog(interactDialog));
            }
        }
    }
}
