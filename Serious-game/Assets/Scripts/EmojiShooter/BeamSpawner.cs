using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSpawner : MonoBehaviour
{
    public GameObject beam;
    public SpaceshipController spaceshipController;
    public float beamStart = 1f;
    public float spawnTime = 1f;
    private float spawnTimeLeft = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && spawnTimeLeft <= 0)
        {
            Instantiate(beam, new Vector2(spaceshipController.transform.position.x + beamStart, spaceshipController.transform.position.y), Quaternion.identity);
            spawnTimeLeft = spawnTime;
        }

        spawnTimeLeft -= Time.deltaTime;
        spawnTimeLeft = Mathf.Max(0, spawnTimeLeft);
    }
}
