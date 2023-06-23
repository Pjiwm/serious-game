using PlayerAndMovement;
using UnityEngine;

namespace SceneLoading
{
    public class SceneLoaderTrigger : MonoBehaviour
    {
        [SerializeField] private SceneLoader.Scenes sceneToLoad;

        public float toXPos;
        public float toYPos;

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerPrefs.SetFloat(PlayerPositionPrefs.X, toXPos);
            PlayerPrefs.SetFloat(PlayerPositionPrefs.Y, toYPos);
            PlayerPrefs.Save();
            Debug.Log("Saved player position to " + toXPos + ", " + toYPos);
            SceneLoader.LoadScene(sceneToLoad);
        }

    }
}
