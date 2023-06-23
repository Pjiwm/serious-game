using SceneLoading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas startMenuCanvas;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private ObstacleSpawner backwardsObstacleSpawner;
    private bool _isMenu = true;


    public void Start()
    {
        obstacleSpawner.enabled = false;
        backwardsObstacleSpawner.enabled = false;
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
            SceneLoader.LoadScene(SceneLoader.Scenes.Level3);
        }
    }


    public void StartGame()
    {
        // Disable the start menu canvas and enable the obstacle spawner
        startMenuCanvas.enabled = false;
        obstacleSpawner.enabled = true;
        backwardsObstacleSpawner.enabled = true;
        _isMenu = false;
        ScoreManager.ResetScore();
    }

    public void GameOver()
    {
        // Enable the game over canvas and disable the obstacle spawner
        gameOverCanvas.enabled = true;
        obstacleSpawner.enabled = false;
        backwardsObstacleSpawner.enabled = false;
        _isMenu = true;

        // Play death sound
        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

}