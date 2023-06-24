using System.Collections;
using SceneLoading;
using UnityEngine;

namespace Menu
{
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

        public void NewGame()
        {
            PlayerPrefs.DeleteAll();
            audioSource.Play();
            StartCoroutine(StartGameCoroutine());
        }
        
        private static IEnumerator StartGameCoroutine()
        {
            yield return new WaitForSeconds(2);
            SceneLoader.LoadScene(SceneLoader.Scenes.Level1);
        }
    }
}
