using PlayerAndMovement;
using UnityEngine;

namespace TeleportingAndSpawning
{
    public class SpawnPointChecker : MonoBehaviour
    {
        [SerializeField] private Transform playerLocation;

        private void Start()
        {
            var x = PlayerPrefs.GetFloat(PlayerPositionPrefs.X, playerLocation.position.x);
            var y = PlayerPrefs.GetFloat(PlayerPositionPrefs.Y, playerLocation.position.y);
            playerLocation.position = new Vector2(x, y);
            PlayerPrefs.DeleteKey(PlayerPositionPrefs.X);
            PlayerPrefs.DeleteKey(PlayerPositionPrefs.Y);
            PlayerPrefs.Save();
            Debug.Log("Spawning player to " + x + ", " + y);
        }
    }
}
