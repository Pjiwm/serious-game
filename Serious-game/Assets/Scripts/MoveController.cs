using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _rigidBody2D;
    private float MoveDistance => speed * Time.fixedDeltaTime;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void HandleMovement(Vector2 inputVector)
    {
        if (inputVector != Vector2.zero)
        {
            _rigidBody2D.MovePosition(_rigidBody2D.position + inputVector * MoveDistance);
        }
    }
}