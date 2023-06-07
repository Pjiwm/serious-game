using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private GameInput gameInput;
    private float MoveDistance => speed * Time.deltaTime;
    private bool _isWalking = false;
    void Update()
    {
        HandleMovement();
    }
    
    private void HandleMovement()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector2(inputVector.x, inputVector.y);
        if (!CanMove(moveDir))
        {
            var moveDirX = new Vector2(moveDir.x, 0).normalized;
            var moveDirY = new Vector2(0, 0).normalized;
            
            if (CanMove(moveDirX)) moveDir = moveDirX;
            if (CanMove(moveDirY)) moveDir = moveDirY;
        }
        if (CanMove(moveDir))
        {
            transform.position += (moveDir * MoveDistance).ToVector3();
        }

        _isWalking = moveDir != Vector2.zero;
    }
    private bool CanMove(Vector2 moveDirection)
    {
        var playerSize = new Vector2(1, 1);
        var rotatedAngle = 0f;
        var moveDistance = speed * Time.deltaTime;
        return !Physics2D.BoxCast(transform.position, playerSize, rotatedAngle, moveDirection, moveDistance);
    }
}
