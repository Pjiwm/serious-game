using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class SceneLoaderTrigger : MonoBehaviour
{
    [SerializeField] private SceneLoader.Scenes sceneToLoad;

    public float toXPos = 0;
    public float toYPos = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPrefs.SetFloat(PlayerPositionPrefs.X, toXPos);
        PlayerPrefs.SetFloat(PlayerPositionPrefs.Y, toYPos);
        PlayerPrefs.Save();
        SceneLoader.LoadScene(sceneToLoad);
    }

}
