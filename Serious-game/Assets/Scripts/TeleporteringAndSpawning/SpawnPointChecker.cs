using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPointChecker : MonoBehaviour
{
    [SerializeField] private Transform playerLocation;
    void Start()
    {
        float x = PlayerPrefs.GetFloat(PlayerPositionPrefs.X, playerLocation.position.x);
        float y = PlayerPrefs.GetFloat(PlayerPositionPrefs.Y, playerLocation.position.y);
        playerLocation.position = new Vector2(x, y);
        PlayerPrefs.DeleteKey(PlayerPositionPrefs.X);
        PlayerPrefs.DeleteKey(PlayerPositionPrefs.Y);
        PlayerPrefs.Save();
        Debug.Log("Spawning player to " + x + ", " + y);
    }
}
