using Eflatun.SceneReference;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public bool hasTransition;
    public SceneReference menuScene;

    private Animator animator;
    private GameManagerFSM gameManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManagerFSM>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("character_nearby", true);

            if (hasTransition)
            {
                gameManager.NextActiveScene = menuScene;
                gameManager.ChangeState(gameManager.GetComponent<ReturnToMenuGameState>());
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("character_nearby", false);
        }
    }
}
