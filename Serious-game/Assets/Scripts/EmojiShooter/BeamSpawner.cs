using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSpawner : MonoBehaviour
{
    [SerializeField] private GameObject beam;
    [SerializeField] private SpaceshipController spaceshipController;
    [SerializeField] private float beamStart = 1f;
    [SerializeField] private float spawnTime = 1f;
    
    private float _spawnTimeLeft = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _spawnTimeLeft <= 0)
        {
            var audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            Instantiate(beam, new Vector2(spaceshipController.transform.position.x + beamStart, spaceshipController.transform.position.y), Quaternion.identity);
            _spawnTimeLeft = spawnTime;

        }

        _spawnTimeLeft -= Time.deltaTime;
        _spawnTimeLeft = Mathf.Max(0, _spawnTimeLeft);
    }
}
