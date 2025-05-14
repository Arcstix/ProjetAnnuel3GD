using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class InteractiveTarget : MonoBehaviour
{
    private PlayerTargetSystem player;
    
    [Header("Visible Renderer")]
    [Tooltip("Si le renderer n'est pas sur le parent alors il faut définir ce qui doit être visible sinon on met rien")]
    public CheckVisibleRenderer checkVisibleRenderer;
    
    [Header("UI Target")]
    [Tooltip("Sert à placé l'auto target ou l'on souhaite si c'est null il prendra l'origine de l'objet")]
    public Transform targetTranform;
    
    [Header("Booleans")]
    public bool isAvailable = true;
    public bool isReachable = false;
    public bool isRendered = false;
    
    [Header("Interaction Type")]
    public InteractorType _currentType = InteractorType.None;
    
    private void Start()
    {
        player = FindObjectOfType<PlayerTargetSystem>();
    }

    private void Update()
    {
        if (!isAvailable) return;
        
        Vector3 playerToObject = (targetTranform.position - player.transform.position).normalized;
        float dot = Vector3.Dot(Camera.main.transform.forward, playerToObject);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        bool inVisionRange = angle < player.visionAngle;

        if (Vector3.Distance(targetTranform.position, player.transform.position) < player.maxReachDistance && inVisionRange && !isReachable)
        {
            if (player.targets.Contains(this))
            {
                isReachable = true;
                player.reachableTargets.Add(this);
            }
        }

        if ((Vector3.Distance(targetTranform.position, player.transform.position) > player.maxReachDistance || !inVisionRange) && isReachable)
        {
            isReachable = false;
            if (player.reachableTargets.Contains(this))
                player.reachableTargets.Remove(this);

            if (player.currentTarget == this)
            {
                player.ClearCurrentTarget();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isRendered)
        {
            if (IsVisibleFrom(player.transform))
            {
                if(!player.targets.Contains(this))
                    player.targets.Add(this);
            }
            else
            {
                isReachable = false;
                player.targets.Remove(this);
                if(player.reachableTargets.Contains(this))
                    player.reachableTargets.Remove(this);
                
                if(player.currentTarget == this)
                {
                    player.ClearCurrentTarget();
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (checkVisibleRenderer != null)
        {
            isRendered = checkVisibleRenderer.GetRendererStatus();
        }
    }

    private bool IsVisibleFrom(Transform target)
    {
        Vector3 direction = (targetTranform.position - target.transform.position).normalized;
        Ray ray = new Ray(target.transform.position, direction);
        RaycastHit hit;
        
        // Raycast qui touche tout sauf les layers "Player" et "Interactable"
        if (Physics.Raycast(ray, out hit, Vector3.Distance(targetTranform.position, target.position), ~LayerMask.GetMask("Player", "Interactable")))
        {
            // Si le premier objet touché est bien la cible, elle est visible
            return hit.transform == transform;
        }
        return false;
    }
    
    private void OnBecameVisible()
    {
        if (!player.targets.Contains(this) && isAvailable)
        {
            isRendered = true;
        }
    }

    private void OnBecameInvisible()
    {
        isRendered = false;
        if (player.targets.Contains(this))
        {
            player.targets.Remove(this);
            if(player.reachableTargets.Contains(this))
                player.reachableTargets.Remove(this);
        }

        if(player.currentTarget == this)
        {
            player.ClearCurrentTarget();
        }
    }

    public void EnableInteraction()
    {
        isAvailable = true;
    }
    
    public void DisableInteraction()
    {
        isAvailable = false;
        isReachable = false;
        if (player.targets.Contains(this))
        {
            player.targets.Remove(this);
            if(player.reachableTargets.Contains(this))
                player.reachableTargets.Remove(this);
        }

        if (player.currentTarget == this)
        {
            player.ClearCurrentTarget();
        }
    }
    
    public void SetInteractionType(InteractorType type)
    {
        _currentType = type;
    }

    public InteractorType GetCurrentType()
    {
        return _currentType;
    }
}
