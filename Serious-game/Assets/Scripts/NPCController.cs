using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
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
        Debug.Log("Hello! I am looking for my dog. Have you seen him?");
    }
}
