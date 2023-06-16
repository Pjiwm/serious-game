using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas gameOverCanvas;
    public Canvas startMenuCanvas;
    public ObstacleSpawner obstacleSpawner;
    private bool _isMenu = true;


    public void Start()
    {
        obstacleSpawner.enabled = false;
        gameOverCanvas.enabled = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isMenu)
        {
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Loading computer scene");
            SceneLoader.LoadScene(SceneLoader.Scenes.Level3);
        }
    }


    public void StartGame()
    {
        Debug.Log("Start Game");
        // Disable the start menu canvas and enable the obstacle spawner
        startMenuCanvas.enabled = false;
        obstacleSpawner.enabled = true;
        _isMenu = false;
        ScoreManager.ResetScore();
    }

    public void GameOver()
    {
        // Enable the game over canvas and disable the obstacle spawner
        Debug.Log("Game Over");
        gameOverCanvas.enabled = true;
        obstacleSpawner.enabled = false;
        _isMenu = true;

        // Play death sound
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

}