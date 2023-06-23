using UnityEngine;

namespace NPCControllers
{
    public class LostFriendNPCController : NPCController
    {
        [SerializeField] private Dialog.Dialog interactionDialog;

        private void Start()
        {
            if(PlayerPrefs.HasKey(PlayerPrefKeys.MazeStarted)) {
                gameObject.SetActive(false);
            }
        }

        protected override void OnInteract()
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(interactionDialog));
            PlayerPrefs.SetInt(PlayerPrefKeys.MazeStarted, 1);
            PlayerPrefs.SetInt(PlayerPrefKeys.MazeFriend, 1);
            PlayerPrefs.Save();
        }
    
    }
}
