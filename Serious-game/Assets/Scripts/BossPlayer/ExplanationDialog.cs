using Unity.VisualScripting;
using UnityEngine;

namespace BossPlayer
{
    public class ExplanationDialog : MonoBehaviour
    {
        [SerializeField] private Dialog.Dialog interactDialog;
        public Healthbar healthBar;

        private void Start()
        {
            PauseGame();
            StartCoroutine(DialogManager.Instance.ShowDialog(interactDialog));
            DialogManager.Instance.OnCloseDialog += OnCloseDialog;
        }

        private static void OnCloseDialog()
        {
            DialogManager.Instance.OnCloseDialog -= OnCloseDialog;
            ResumeGame();
        }

        private static void PauseGame()
        {
            Time.timeScale = 0;
        }
        
        private static void ResumeGame ()
        {
            Time.timeScale = 1;
        }
    }
}