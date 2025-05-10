using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHubAction : MonoBehaviour
{
    [SerializeField] private PlayerUIManager uiManager;
    [SerializeField] private Image interactionImage;

    private void Awake()
    {
        uiManager.OnInteract += OnInteraction;
        interactionImage.color = Color.grey;
    }

    private void OnInteraction(bool value)
    {
        //TODO : Afficher l'image avec une tinte verte et des particules
        interactionImage.color = value ? Color.white : Color.grey;
    }
}
