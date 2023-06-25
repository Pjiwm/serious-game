using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteOptions;
    [SerializeField] private float speed = 5;
    [SerializeField] private GameObject destroyedObject;

    [SerializeField] private float xDir = -1;

    private void Start()
    {
        if (spriteOptions.Length > 0)
        {
            var randomIndex = Random.Range(0, spriteOptions.Length);
            var randomSprite = spriteOptions[randomIndex];

            // Set the sprite of the object
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = randomSprite;
        }
        else
        {
            Debug.LogError("No sprite options assigned to RandomSpriteObject!");
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.right * (xDir * speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Beam")) return;
        
        Destroy(gameObject);
        Destroy(other.gameObject);
        Instantiate(destroyedObject, transform.position, Quaternion.identity);
        // ScoreManager.score += 1;
        ScoreManager.AddScore();
    }

}
