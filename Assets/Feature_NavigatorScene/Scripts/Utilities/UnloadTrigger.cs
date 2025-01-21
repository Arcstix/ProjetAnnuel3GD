using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Eflatun.SceneReference;

public class UnloadTrigger : MonoBehaviour
{
    [SerializeField] private SceneReference sceneToUnload;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.UnloadSceneAsync(sceneToUnload.BuildIndex);
        }
    }
}
