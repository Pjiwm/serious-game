using UnityEngine;

namespace NPCControllers
{
    public class BasicNPCController : NPCController
    {
        [SerializeField] private Dialog.Dialog interactDialog;
        protected override void OnInteract()
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(interactDialog));
        }
    }
}