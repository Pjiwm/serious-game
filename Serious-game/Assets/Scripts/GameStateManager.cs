using System;
using UnityEngine;

public enum GameState { Roaming, InDialogue, OnMinigame}
[CreateAssetMenu(fileName = "GameStateManager", menuName = "ScriptableObjects/GameStateManager", order = 1)]
public class GameStateManager : ScriptableObject
{
    public Action OnRoaming;
    public Action OnInDialogue;
    public Action OnMinigame;
    public GameState State { get; private set; }

    private void OnEnable()
    {
        State = GameState.Roaming;
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
        State = newState;
    }
}
