using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    public float speed = 50;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
