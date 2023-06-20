using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public Sprite[] spriteOptions;
    public float speed = 5;
    public GameObject destroyedObject;

    public float xDir = -1;

    void Start()
    {
        if (spriteOptions.Length > 0)
        {
            int randomIndex = Random.Range(0, spriteOptions.Length);
            Sprite randomSprite = spriteOptions[randomIndex];

            // Set the sprite of the object
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = randomSprite;
        }
        else
        {
            Debug.LogError("No sprite options assigned to RandomSpriteObject!");
        }
    }

    void Update()
    {
        transform.Translate(xDir * Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (other.CompareTag("Beam"))
        {
            Debug.Log("Beam hit");
            Destroy(gameObject);
            Destroy(other.gameObject);
            Instantiate(destroyedObject, transform.position, Quaternion.identity);
            // ScoreManager.score += 1;
            ScoreManager.AddScore();
        }
    }

}
