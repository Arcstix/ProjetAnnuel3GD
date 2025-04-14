using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCycler : MonoBehaviour
{
    public string[] sceneNames = { "SceneDa", "SceneDa2", "SceneDAObjet1", "SceneDAObjet2" }; // Liste des scènes
    private int currentSceneIndex = 0; // Index actuel

    void Start()
    {
        // Détecte automatiquement l'index de la scène actuelle
        string currentScene = SceneManager.GetActiveScene().name;
        currentSceneIndex = System.Array.IndexOf(sceneNames, currentScene);
        
        // Sécurité : si la scène actuelle n'est pas dans le tableau, démarre à 0
        if (currentSceneIndex == -1) 
        {
            currentSceneIndex = 0;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Détecte l'appui sur Tab
        {
            currentSceneIndex = (currentSceneIndex + 1) % sceneNames.Length; // Passe à la scène suivante en boucle
            SceneManager.LoadScene(sceneNames[currentSceneIndex]); // Charge la scène correspondante
        }
    }
}