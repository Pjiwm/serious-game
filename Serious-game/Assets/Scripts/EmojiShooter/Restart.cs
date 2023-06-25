using SceneLoading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.R)) return;
        
        ScoreManager.ResetScore();
        SceneLoader.LoadScene(SceneLoader.Scenes.EmojiShooter);
    }
}