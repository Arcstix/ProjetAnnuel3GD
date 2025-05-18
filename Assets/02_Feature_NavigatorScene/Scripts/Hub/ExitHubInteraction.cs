using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ExitHubInteraction : MonoBehaviour
{
    private PlayerUIManager player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player == null)
            {
                player = other.GetComponent<PlayerUIManager>();
            }
            
            player.SetInteract(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player == null)
            {
                player = other.GetComponent<PlayerUIManager>();
            }
            
            player.SetInteract(false);
        }
    }
}
