using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas gameOverCanvas;
    public Canvas startMenuCanvas;
    public ObstacleSpawner obstacleSpawner;
    private bool isMenu = true;


    public void Start()
    {
        obstacleSpawner.enabled = false;
        gameOverCanvas.enabled = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isMenu)
        {
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Loading computer scene");
            SceneManager.LoadScene("Level3map");
        }
    }


    public void StartGame()
    {
        Debug.Log("Start Game");
        // Disable the start menu canvas and enable the obstacle spawner
        startMenuCanvas.enabled = false;
        obstacleSpawner.enabled = true;
        isMenu = false;
        ScoreManager.ResetScore();
    }

    public void GameOver()
    {
        // Enable the game over canvas and disable the obstacle spawner
        Debug.Log("Game Over");
        gameOverCanvas.enabled = true;
        obstacleSpawner.enabled = false;
        isMenu = true;
    }

}