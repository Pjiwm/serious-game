using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void StartGame()
    {
        audioSource.Play();
        StartCoroutine(StartGameCoroutine());
    }

    public void Quit()
    {
        Application.Quit();
    }
        
    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Level1");
    }
}
