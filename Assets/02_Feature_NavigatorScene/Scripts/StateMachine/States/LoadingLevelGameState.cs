using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingLevelGameState : GameState
{
    public GameObject loadingMenuGO;

    [SerializeField] private Scrollbar loadingScrollbar;
    //Wait at least this amount of time :
    [SerializeField] private float minLoadingTime = 0.5f;
    
    private AsyncOperation asyncLoad;
    private AsyncOperation asyncUnload;
    private float minTransitionTime;
    private bool isUnloading = false;
    private bool isReloading = false;
    
    public override void Enter()
    {
        minTransitionTime = Time.time + minLoadingTime;
        loadingMenuGO.SetActive(true);
        asyncLoad = SceneManager.LoadSceneAsync(gameManager.NextActiveScene.BuildIndex, LoadSceneMode.Single);
        // if (gameManager.NextActiveScene.BuildIndex == gameManager.CurrentActiveScene.BuildIndex)
        // {
        //     isReloading = true;
        //     asyncLoad = SceneManager.LoadSceneAsync(gameManager.NextActiveScene.BuildIndex, LoadSceneMode.Single);
        // }
        // else
        // {
        //     isReloading = false;
        //     asyncLoad = SceneManager.LoadSceneAsync(gameManager.NextActiveScene.BuildIndex, LoadSceneMode.Additive);
        // }
    }

    public override void Tick()
    {
        if (asyncLoad.isDone && Time.time >= minTransitionTime)
        {
            gameManager.LastActiveScene = gameManager.CurrentActiveScene;
            gameManager.CurrentActiveScene = gameManager.NextActiveScene;
            SceneManager.SetActiveScene(gameManager.CurrentActiveScene.LoadedScene);
            gameManager.ChangeState(GetComponent<LevelInitializationGameState>());
        }
        
        loadingScrollbar.size = asyncLoad.progress;
        
        // if (asyncLoad.isDone && !isUnloading)
        // {
        //     gameManager.LastActiveScene = gameManager.CurrentActiveScene;
        //     gameManager.CurrentActiveScene = gameManager.NextActiveScene;
        //     SceneManager.SetActiveScene(gameManager.CurrentActiveScene.LoadedScene);
        //     if (!isReloading)
        //     {
        //         UnloadLastScene();
        //         isUnloading = true;
        //     }
        // }
        //
        // if (asyncUnload != null)
        // {
        //     if (asyncUnload.isDone && Time.time >= minTransitionTime)
        //     {
        //         isUnloading = false;
        //         gameManager.ChangeState(GetComponent<LevelInitializationGameState>());
        //     }
        // }
        //
        // if (asyncLoad.isDone && isReloading && Time.time >= minTransitionTime)
        // {
        //     gameManager.ChangeState(GetComponent<LevelInitializationGameState>());
        // }
        //
        // if (asyncUnload != null)
        // {
        //     // Show loading bar/ image... 
        //     loadingScrollbar.size = asyncLoad.progress / 2 + asyncUnload.progress / 2;
        // }
        // else
        // {
        //     if (isReloading)
        //     {
        //         loadingScrollbar.size = asyncLoad.progress;
        //     }
        //     else
        //     {
        //         loadingScrollbar.size = asyncLoad.progress / 2;
        //     }
        // }
    }

    public override void Exit()
    {
        loadingMenuGO.SetActive(false);
    }

    private void UnloadLastScene()
    {
        asyncUnload = SceneManager.UnloadSceneAsync(gameManager.LastActiveScene.BuildIndex);
        foreach (var chunk in gameManager.chunkList)
        {
            SceneManager.UnloadSceneAsync(chunk.BuildIndex);
        }
        gameManager.chunkList.Clear();
    }
}