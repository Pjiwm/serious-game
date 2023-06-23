using SceneLoading;
using UnityEngine;
using UnityEngine.Serialization;

namespace TeleportingAndSpawning
{
    public class EndBossTeleporter : MonoBehaviour
    {
        [FormerlySerializedAs("EndbossTeleporterDialog")] [SerializeField] private Dialog.Dialog endbossTeleporterDialog;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(endbossTeleporterDialog));
            DialogManager.Instance.OnCloseDialog += LoadLevel;
        }
        private static void LoadLevel()
        {
            DialogManager.Instance.OnCloseDialog -= LoadLevel;
            SceneLoader.LoadScene(SceneLoader.Scenes.EndBoss);
        }
    }
}
