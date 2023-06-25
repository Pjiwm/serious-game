using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishEvasion : MonoBehaviour
{
    /// <summary>
    /// This script is used on the fish and controls it moving up and down
    /// the water to seem like it is evading the catchingbar.
    /// </summary>
    
    //The max and min heights we can go (Should be Water_Top and Water_Bottom objects)
    [SerializeField] private RectTransform maxHeight;
    [SerializeField] private RectTransform minHeight;
    
    [Range(0, 5f)] [SerializeField] private float moveSpeed; //How fast the fish move
    
    [SerializeField] private float maxWaitTime, minWaitTime; //How long the fish waits before moving again
    
    private Vector3 _currentDestination; //Where the fish is moving towards

    private bool _waiting; //Used when waiting for a new destination

    private void Start() {
        _currentDestination = RandomDestination(); //Give the fish a random direction to go to
    }

    private void Update() {
        transform.position = Vector3.Lerp(transform.position, _currentDestination, moveSpeed  * Time.deltaTime); //Lerp towards the fishes current destination
        
        if (Vector3.Distance(transform.position, _currentDestination) <= 1f && !_waiting) { //If we get close and arent already waiting then get a new destination
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait() { //Used to let the fish wait a bit before getting a new destination
        _waiting = true;
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        _currentDestination = RandomDestination();
        _waiting = false;
    }

    private Vector3 RandomDestination() {
        //Pick a random height to go to, between the top and bottom but they are offset using the height of the fish so it doesnt overlpa
        var rectTDelta = GetComponent<RectTransform>().sizeDelta;
        
        var maxUp = maxHeight.position.y - rectTDelta.y/2;
        var maxDown = minHeight.position.y + rectTDelta.y/2;
        
        var newHeight = Random.Range(maxUp, maxDown);

        return new Vector3(transform.position.x, newHeight, transform.position.z);
    }
}
