using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleSpawner : MonoBehaviour
{
    [FormerlySerializedAs("Obstacle")] [SerializeField] private GameObject obstacle;
    [SerializeField] private float spawnTime = 2f;
    [SerializeField] private int spawnRange = 4;
    
    private float _spawnTimeLeft;

    private void Update()
    {
        if (_spawnTimeLeft <= 0)
        {
            var objectCount = Random.Range(1, spawnRange);
            
            for (var i = 0; i < objectCount; i++)
            {
                Instantiate(obstacle, new Vector2(transform.position.x, Random.Range(-3, 3)), Quaternion.identity);
                _spawnTimeLeft = spawnTime;
            }
        }

        _spawnTimeLeft -= Time.deltaTime;
        _spawnTimeLeft = Mathf.Max(0, _spawnTimeLeft);
    }
}
