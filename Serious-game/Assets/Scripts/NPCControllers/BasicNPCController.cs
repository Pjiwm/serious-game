using UnityEngine;

public class BasicNPCController : NPCController
{
    [SerializeField] private Dialog interactDialog;
    protected override void OnInteract()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(interactDialog));
    }
}