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
        
        private static IEnumerator StartGameCoroutine()
        {
            yield return new WaitForSeconds(2);
            SceneLoader.LoadScene(SceneLoader.Scenes.Level1);
        }
    }
}
