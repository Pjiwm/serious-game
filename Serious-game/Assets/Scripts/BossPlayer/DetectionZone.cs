using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{

    public List<Collider2D> detectedObjects = new List<Collider2D>();
    private Collider2D collider;
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            detectedObjects.Add(other);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        detectedObjects.Remove(other);
    }
}
