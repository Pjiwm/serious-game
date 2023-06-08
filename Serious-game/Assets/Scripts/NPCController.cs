using DefaultNamespace;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialog interactDialog;
    private Animator animator;
    
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IsWalking, true);
    }

    public void Select()
    {
        Debug.Log("Could you help me?");
    }

    public void Interact()
    {
        Debug.Log(DialogManager.Instance);
        StartCoroutine(DialogManager.Instance.ShowDialog(interactDialog));
    }
}
