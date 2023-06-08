using System;
using UnityEngine;

public enum GameState { Roaming, InDialogue}
public class GameStateController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
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
                playerController.DeActivateMovement();
                break;
            case GameState.Roaming:
                playerController.ActivateMovement();
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
            playerController.HandleUpdate();
        }
    }
}
