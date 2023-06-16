using System;
using UnityEngine;
using UnityEngine.Events;

public enum GameState { Roaming, InDialogue, OnMinigame}
public class GameStateController : Singleton<GameStateController>
{
    [SerializeField] private PlayerController playerController;
    public Action OnRoaming;
    public Action OnInDialogue;
    public Action OnMinigame;
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
    public void ChangeToState(GameState newState)
    {
        switch (newState)
        {
            case GameState.InDialogue:
                OnInDialogue?.Invoke();
                break;
            case GameState.Roaming:
                OnRoaming?.Invoke();
                break;
            case GameState.OnMinigame:
                OnMinigame?.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        state = newState;
    }

    private void FixedUpdate()
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
