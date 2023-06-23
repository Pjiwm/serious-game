using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.R)) return;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ScoreManager.ResetScore();
    }
}