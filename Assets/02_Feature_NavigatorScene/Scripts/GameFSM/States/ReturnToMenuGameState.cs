using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMenuGameState : GameState
{
    public GameObject loadingMenu;

    [SerializeField] private Scrollbar loadingScrollbar;
    [SerializeField] private float minLoadingTime = 0.5f;

    private AsyncOperation asyncLoad;
    private float minTransitionTime;

    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        minTransitionTime = Time.time + minLoadingTime;
        loadingMenu.SetActive(true);
        asyncLoad = SceneManager.UnloadSceneAsync(fsm.CurrentActiveScene.BuildIndex);
        foreach (var chunk in fsm.chunkList)
        {
            SceneManager.UnloadSceneAsync(chunk.BuildIndex);
        }
        fsm.chunkList.Clear();
    }

    public override void Tick()
    {
        if (asyncLoad.isDone && Time.time >= minTransitionTime)
        {
            fsm.CurrentActiveScene = fsm.NextActiveScene;
            SceneManager.SetActiveScene(fsm.CurrentActiveScene.LoadedScene);
            fsm.ChangeState(GetComponent<MainMenuGameState>());
        }

        loadingScrollbar.size = asyncLoad.progress;
    }

    public override void Exit()
    {
        loadingMenu.SetActive(false);
    }
}