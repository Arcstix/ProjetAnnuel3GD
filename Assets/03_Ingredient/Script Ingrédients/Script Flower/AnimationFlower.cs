using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFlower : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private ProjectileInteraction projectileInteraction;
    
    private void Awake()
    {
        projectileInteraction = GetComponent<ProjectileInteraction>();    
    }

    private void Start()
    {
        if (projectileInteraction != null)
        {
            projectileInteraction.OnPlatformInteract += OpenFlower;
        }
        else
        {
            OpenFlower();
        }
    }

    public void CloseFlower()
    {
        animator.SetBool("Eclosion", false);
    }
    
    public void OpenFlower()
    {
        animator.SetBool("Eclosion", true);
    }
}
