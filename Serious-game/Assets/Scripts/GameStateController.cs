using System;
using UnityEngine;
using UnityEngine.Events;

public enum GameState { Roaming, InDialogue}
public class GameStateController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public UnityEvent OnRoaming;
    public UnityEvent OnInDialogue;
    
    private GameState state = GameState.Roaming;

    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            ChangeToState(GameState.InDialogue);
            
        };
        DialogManager.Instance.OnCloseDialog += () =>
        {
            ChangeToState(GameState.Roaming);
        };
    }
    private void ChangeToState(GameState newState)
    {
        switch (newState)
        {
            case GameState.InDialogue:
                OnInDialogue?.Invoke();
                break;
            case GameState.Roaming:
                OnRoaming?.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        state = newState;
    }

    private void Update()
    {
        if (state == GameState.Roaming)
        {
            if (playerController != null)
            {
                playerController.HandleUpdate();
            }
                
        }
    }
}
