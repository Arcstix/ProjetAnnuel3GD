using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Eflatun.SceneReference;

public class LoadTrigger : MonoBehaviour
{
    [SerializeField] private SceneReference sceneToLoad;

    private GameManagerFSM gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManagerFSM>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sceneToLoad.LoadedScene.isLoaded == false)
            {
                SceneManager.LoadSceneAsync(sceneToLoad.BuildIndex, LoadSceneMode.Additive);
                gameManager.AddChunk(sceneToLoad);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sceneToLoad.LoadedScene.isLoaded == true)
            {
                Instantiate(other.gameObject, sceneToLoad.LoadedScene);
                SceneManager.UnloadSceneAsync(sceneToLoad.BuildIndex);
                gameManager.RemoveChunk(sceneToLoad);
            }
        }
    }
}
