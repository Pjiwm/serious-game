using PlayerAndMovement;
using UnityEngine;
using UnityEngine.Serialization;

namespace NPCControllers
{
    public class LostFriendNPCController : NPCController
    {
        [SerializeField] private Dialog.Dialog interactionDialog;
        [SerializeField] private Dialog.Dialog friendMadeDialog;
        [SerializeField] private Dialog.Dialog alreadySpokenToDialog;

        private void Start()
        {
            if (PlayerPrefs.HasKey(PlayerPrefKeys.MazeFriendMade))
            {
                gameObject.SetActive(false);
            }
        }

        protected override void OnInteract()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefKeys.LostMazeFriendSpokenTo))
            {
                PlayerPrefs.SetInt(PlayerPrefKeys.LostMazeFriendSpokenTo, 1);
                PlayerPrefs.Save();
                StartCoroutine(DialogManager.Instance.ShowDialog(interactionDialog)); 
            }
            else StartCoroutine(DialogManager.Instance.ShowDialog(alreadySpokenToDialog));
        }

        void LateUpdate()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefKeys.LostMazeFriendDelivered) && transform.position.x < -0.36f && transform.position.x > -0.44f && transform.position.y > 0.345f && transform.position.y < 0.7f)
            {
                PlayerPrefs.SetInt(PlayerPrefKeys.LostMazeFriendDelivered, 1);
                PlayerPrefs.SetInt(PlayerPrefKeys.MazeFriendSpokenTo, 1);
                PlayerPrefs.Save();

                GetComponent<FollowingNPCController>().enabled = false;
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().StopFootsteps();

                StartCoroutine(DialogManager.Instance.ShowDialog(friendMadeDialog));
            }
            else if (PlayerPrefs.HasKey(PlayerPrefKeys.LostMazeFriendDelivered) && (transform.position.y < 0.65f))
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 0.65f), 0.5f * Time.deltaTime);
            }
            else if (PlayerPrefs.HasKey(PlayerPrefKeys.LostMazeFriendDelivered) && transform.position.y >= 0.65f)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
