using UnityEngine;
using UnityEngine.Serialization;

namespace NPCControllers
{
    public class MazeFriendNPCController : NPCController
    {
        [SerializeField] private Dialog.Dialog interactDialog;
        [FormerlySerializedAs("_statsManager")] [SerializeField] private StatsManager statsManager;
        [SerializeField] private Dialog.Dialog friendsDialog;


        protected override void OnInteract()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefKeys.Maze))
            {
                StartCoroutine(DialogManager.Instance.ShowDialog(interactDialog));
            }
            else
            {
                if (PlayerPrefs.HasKey(PlayerPrefKeys.MazeFriend))
                {
                    StartCoroutine(DialogManager.Instance.ShowDialog(friendsDialog));
                }
                else
                {
                    var friends = PlayerPrefs.GetInt(PlayerPrefKeys.Friends, 0);
                    friends++;
                    StatsManager.UpdatePref(PlayerPrefKeys.Friends, friends);
                    statsManager.RequestUpdate();
                    PlayerPrefs.SetInt(PlayerPrefKeys.MazeFriend, 1);
                    PlayerPrefs.Save();
                    StartCoroutine(DialogManager.Instance.ShowDialog(friendsDialog));
                }
            }
        }
    
    }
}
