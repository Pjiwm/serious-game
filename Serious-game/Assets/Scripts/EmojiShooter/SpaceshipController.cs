using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SpaceshipController : MonoBehaviour

{
    private Vector2 targetPos = new Vector2(-1f, 0.0f);
    [SerializeField] private float yIncrement = 1;
    [SerializeField] private float speed = 10;
    [SerializeField] private float maxUp = 3.5f;
    [SerializeField] private float maxDown = -3.5f;

    [SerializeField] private short health = 2;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject destroyedObject;

    [SerializeField] private float invincibilityDuration = 2f; // Duration of invincibility frames
    
    [SerializeField] private Sprite damagedSprite;
    
    private bool _isInvincible; // Flag to indicate if the spaceship is invincible
    private float _invincibilityTimer;

    
    private Sprite _normalSprite;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _normalSprite = _spriteRenderer.sprite;
    }

    private void Update()
    {
        if (_isInvincible)
        {
            _invincibilityTimer += Time.deltaTime;

            if (_invincibilityTimer >= invincibilityDuration)
            {
                _isInvincible = false;
                _spriteRenderer.sprite = _normalSprite;
                _invincibilityTimer = 0f;
            }
        }

        if (health <= 0)
        {
            gameManager.GameOver();
            Destroy(gameObject);
            var instantiatedObject = Instantiate(destroyedObject, transform.position, Quaternion.identity);
            instantiatedObject.transform.localScale = new Vector3(4f, 4f, 1f);
        }

        var position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > maxDown)
        {
            
            targetPos = new Vector2(position.x, position.y - yIncrement);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < maxUp)
        {
            targetPos = new Vector2(transform.position.x, position.y + yIncrement);
        }

        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Obstacle")) return;

        if (!_isInvincible)
        {
            health -= 1;
            _isInvincible = true;
            _spriteRenderer.sprite = damagedSprite;
        }
        Destroy(other.gameObject);
        Instantiate(destroyedObject, other.transform.position, Quaternion.identity);
    }
}
