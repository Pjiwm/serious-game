using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject Obstacle;
    public float spawnTime = 2f;
    private float spawnTimeLeft = 0;

    public int spawnRange = 4;

    void Update()
    {
        if (spawnTimeLeft <= 0)
        {
            int objectCount = Random.Range(1, spawnRange);
            for (int i = 0; i < objectCount; i++)
            {
                Instantiate(Obstacle, new Vector2(transform.position.x, Random.Range(-3, 3)), Quaternion.identity);
                spawnTimeLeft = spawnTime;
            }
        }

        spawnTimeLeft -= Time.deltaTime;
        spawnTimeLeft = Mathf.Max(0, spawnTimeLeft);
    }
}
