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
    private float minTransitionTime;
    
    
    public override void Enter()
    {
        minTransitionTime = Time.time + minLoadingTime;
        loadingMenuGO.SetActive(true);
        asyncLoad = SceneManager.LoadSceneAsync(fsm.NextActiveScene.BuildIndex, LoadSceneMode.Additive);
    }

    public override void Tick()
    {
        if (asyncLoad.isDone && Time.time >= minTransitionTime)
        {
            fsm.CurrentActiveScene = fsm.NextActiveScene;
            SceneManager.SetActiveScene(fsm.CurrentActiveScene.LoadedScene);
            fsm.ChangeState(GetComponent<LevelInitializationGameState>());
        }

        //DONE : show loading bar/ image... 
        loadingScrollbar.size = asyncLoad.progress;
    }

    public override void Exit()
    {
        loadingMenuGO.SetActive(false);
    }
}