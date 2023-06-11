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

    public GameObject destroyedObject;


    public float invincibilityDuration = 2f; // Duration of invincibility frames
    private bool isInvincible = false; // Flag to indicate if the spaceship is invincible
    private float invincibilityTimer = 0f;

    public Sprite damagedSprite;
    private Sprite normalSprite;
    private SpriteRenderer spriteRenderer;

    // public GameManager gameManager;

    // public GameObject gameOverCanvas;
    // public TMP_Text healthText;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalSprite = spriteRenderer.sprite;
        // healthText.text = health.ToString();
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer += Time.deltaTime;

            if (invincibilityTimer >= invincibilityDuration)
            {
                isInvincible = false;
                spriteRenderer.sprite = normalSprite;
                invincibilityTimer = 0f;
            }
        }

        if (health <= 0)
        {
            // ScoreManager.score = 0;
            // gameOverCanvas.SetActive(true);
            Destroy(gameObject);
            GameObject instantiatedObject = Instantiate(destroyedObject, transform.position, Quaternion.identity);
            instantiatedObject.transform.localScale = new Vector3(4f, 4f, 1f);
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
            Debug.Log("Spaceship hit!");
            if (!isInvincible)
            {
                health -= 1;
                isInvincible = true;
                spriteRenderer.sprite = damagedSprite;
            }
            // healthText.text = health.ToString();
            Destroy(other.gameObject);
            Instantiate(destroyedObject, other.transform.position, Quaternion.identity);
        }
    }
}
