using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Envoie les infos aux UI pour l'instant
public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject hubCanvas;
    
    private InputActionMapManager inputManager;
    private PlayerInput playerInput;
    private bool canInteract = false;
    
    public event Action<bool> OnInteract; 
    public event Action<bool> OnLevelSelection;

    public void SetInteract(bool value)
    {
        canInteract = value;
        OnInteract?.Invoke(value);
    }

    public void EnableGameCanvas()
    {
        gameCanvas.SetActive(true);
    }

    public void DisableGameCanvas()
    {
        gameCanvas.SetActive(false);
    }

    public void EnableHubCanvas()
    {
        hubCanvas.SetActive(true);
    }

    public void DisableHubCanvas()
    {
        hubCanvas.SetActive(false);
    }
    
    private void Update()
    {
        if (canInteract)
        {
            CheckInteraction();
        }
    }
    
    private void CheckInteraction()
    {
        //Check input Player and show Level Selection
        if (inputManager == null)
        {
            inputManager = GetComponent<InputActionMapManager>();
        }

        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
        }

        if (inputManager != null)
        {
            if (inputManager.IsInActionMap("Player"))
            {
                // Check Interaction to switch action map
                if (playerInput.actions["Interaction"].WasPerformedThisFrame())
                {
                    inputManager.SwitchToUI();
                    //Afficher sélection niveaux
                    OnLevelSelection?.Invoke(true);
                }
            }

            if (inputManager.IsInActionMap("UI"))
            {
                if (playerInput.actions["Exit"].WasPerformedThisFrame())
                {
                    inputManager.SwitchToPlayer();
                    //Désactiver la sélection niveaux
                    OnLevelSelection?.Invoke(false);
                }
            }
        }
    }
}
