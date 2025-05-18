using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UILevelSelection : MonoBehaviour
{
    [SerializeField] private LevelInitializationGameState initializationGameState;
    
    [SerializeField] private GameObject levelSelection;

    [SerializeField] private Button defaultSelection;
    
    private PlayerUIManager uiManager;
    
    private void OnEnable()
    {
        initializationGameState.OnPlayerInitialized += SubscribePlayer;
        levelSelection.SetActive(false);
    }

    private void OnDisable()
    {
        initializationGameState.OnPlayerInitialized -= SubscribePlayer;
        if (uiManager != null)
        {
            uiManager.OnLevelSelection -= DisplayLevelSelection;
        }
    }

    private void SubscribePlayer(GameObject player)
    {
        uiManager = player.GetComponent<PlayerUIManager>();
        uiManager.OnLevelSelection += DisplayLevelSelection;
    }

    private void DisplayLevelSelection(bool value)
    {
        //TODO : Animation Apparition UI
        levelSelection.SetActive(value);
        StartCoroutine(SelectDefaultNextFrame());
    }

    private IEnumerator SelectDefaultNextFrame()
    {
        yield return null; // attendre une frame
        EventSystem.current.SetSelectedGameObject(null);
        
        yield return null;
        EventSystem.current.firstSelectedGameObject = defaultSelection.gameObject;
        EventSystem.current.SetSelectedGameObject(defaultSelection.gameObject);
    }

    public void OnLevelSelected()
    {
        Debug.Log("Level Selected");
        EventSystem.current.firstSelectedGameObject = null;
        levelSelection.SetActive(false);
    }
}
