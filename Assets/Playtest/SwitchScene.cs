using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string sceneM;
    public string sceneL;
    public string sceneMenu;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SwitchToScene(sceneM);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SwitchToScene(sceneL);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchToScene(sceneMenu);
        }
    }

    void SwitchToScene(string sceneName)
    {
        // Vérifie si la scène est déjà chargée
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single); // charge et remplace
        }
    }
}
