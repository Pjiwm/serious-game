using SceneLoading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using PlayerAndMovement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas startMenuCanvas;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private BeamSpawner beamSpawner;
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
            PlayerPrefs.SetFloat(PlayerPositionPrefs.X, 0.33f);
            PlayerPrefs.SetFloat(PlayerPositionPrefs.Y, 0.45f);
            PlayerPrefs.Save();
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
        _isMenu = true;

        obstacleSpawner.enabled = false;
        beamSpawner.enabled = false;
        backwardsObstacleSpawner.enabled = false;

        // Play death sound
        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

}