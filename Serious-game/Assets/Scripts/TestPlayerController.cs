using System;
using System.Collections;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    public float positionIncrement=1;
    public float moveSpeed;
    private bool isMoving;
    public LayerMask solidObjectsLayer;
    public float radius = 0.05f;

    void Update()
    {
        if(!isMoving)
        {
            var targetPos = transform.position;
            if (Input.GetKey(KeyCode.T)) targetPos.y += positionIncrement;
            if (Input.GetKey(KeyCode.F)) targetPos.x -= positionIncrement;
            if (Input.GetKey(KeyCode.G)) targetPos.y -= positionIncrement;
            if (Input.GetKey(KeyCode.H)) targetPos.x += positionIncrement;

            if(IsWalkable(targetPos))
                StartCoroutine(Move(targetPos));
        }
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, radius, solidObjectsLayer) != null)
            return false;

        return true;
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}
