using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    [SerializeField] private PauseGameState pauseGameState;
    
    [SerializeField] private DataScene hubScene;
    [SerializeField] private DataScene mainMenuScene;
    
    public void QuitInteraction()
    {
        if (SceneManager.GetActiveScene().name == "Hub")
        {
            pauseGameState.ReturnToMenu(mainMenuScene);
        }
        else
        {
            pauseGameState.ReturnToHub(hubScene);
        }
    }
}
