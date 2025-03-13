using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class InteractiveTarget : MonoBehaviour
{
    private PlayerTargetSystem player;

    [Header("Booleans")]
    public bool isAvailable = true;
    public bool isReachable = false;
    public bool isRendered = false;

    [Header("Connections")]
    [SerializeField] Renderer visualRenderer;

    private void Start()
    {
        player = FindObjectOfType<PlayerTargetSystem>();
    }

    private void Update()
    {
        Vector3 playerToObject = (transform.position - player.transform.position).normalized;
        float dot = Vector3.Dot(Camera.main.transform.forward, playerToObject);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        bool inVisionRange = angle < 30;

        if (Vector3.Distance(transform.position, player.transform.position) < player.maxReachDistance && inVisionRange && !isReachable)
        {
            if (player.targets.Contains(this))
            {
                isReachable = true;
                player.reachableTargets.Add(this);
            }
        }

        if ((Vector3.Distance(transform.position, player.transform.position) > player.maxReachDistance || !inVisionRange) && isReachable)
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
    
    private bool IsVisibleFrom(Transform target)
    {
        Vector3 direction = (transform.position - target.transform.position).normalized;
        Ray ray = new Ray(target.transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Vector3.Distance(transform.position, target.position), ~LayerMask.GetMask("Player", "Interactable")))
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
}
