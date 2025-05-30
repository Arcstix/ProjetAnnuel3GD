using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadingLevelGameState : GameState
{
    public GameObject loadingMenuGO;

    [SerializeField] private Scrollbar loadingScrollbar;
    //Wait at least this amount of time :
    [SerializeField] private float minLoadingTime = 0.5f;
    
    private AsyncOperation asyncLoad;
    private float minTransitionTime;
    private bool isReloading = false;
    
    public override void Enter()
    {
        minTransitionTime = Time.time + minLoadingTime;
        loadingMenuGO.SetActive(true);
        asyncLoad = SceneManager.LoadSceneAsync(gameManager.CurrentActiveScene.BuildIndex, LoadSceneMode.Single);
    }
    
        public override void Tick()
    {
        if (asyncLoad.isDone && Time.time >= minTransitionTime)
        {
            gameManager.ChangeState(GetComponent<LevelInitializationGameState>());
        }
        
        loadingScrollbar.size = asyncLoad.progress;
    }

    public override void Exit()
    {
        loadingMenuGO.SetActive(false);
    }

}
