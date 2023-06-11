using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpaceshipController : MonoBehaviour

{
    private Vector2 targetPos = new Vector2(-6.0f, 0.0f);

    public float Yincrement = 1;
    public float speed = 10;
    public float maxUp = 3.5f;
    public float maxDown = -3.5f;

    public short health = 3;

    // public GameManager gameManager;

    // public GameObject gameOverCanvas;
    // public TMP_Text healthText;

    void Start()
    {
        // healthText.text = health.ToString();
    }

    void Update()
    {
        if (health <= 0)
        {
            // ScoreManager.score = 0;
            // gameOverCanvas.SetActive(true);
            Destroy(gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > maxDown)
        {
            Debug.Log("Down");
            targetPos = new Vector2(transform.position.x, transform.position.y - Yincrement);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < maxUp)
        {
            Debug.Log("Up");
            targetPos = new Vector2(transform.position.x, transform.position.y + Yincrement);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            health -= 1;
            // healthText.text = health.ToString();
            Destroy(other.gameObject);
        }
    }
}
