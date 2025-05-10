using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UILevelSelection : MonoBehaviour
{
    [SerializeField] private PlayerUIManager uiManager;
    
    [SerializeField] private GameObject levelSelection;

    [SerializeField] private Button defaultSelection;
    
    private void Awake()
    {
        uiManager.OnLevelSelection += DisplayLevelSelection;
        levelSelection.SetActive(false);
    }

    private void DisplayLevelSelection(bool value)
    {
        //TODO : Animation Apparition UI
        levelSelection.SetActive(value);
        EventSystem.current.SetSelectedGameObject(defaultSelection.gameObject);
    }
}
