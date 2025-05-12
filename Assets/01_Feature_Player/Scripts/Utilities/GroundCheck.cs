using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask groundLayer;

    private PlayerMovementManager movementManager;
    private GroundTypeEnum currentSurface;
    private GroundTypeEnum lastSurface;
    private CapsuleColliderUtility capsuleColliderUtility;

    private void Awake()
    {
        movementManager = GetComponent<PlayerMovementManager>();
    }

    private void Start()
    {
        capsuleColliderUtility = movementManager.CapsuleUtility;
    }

    public GroundTypeEnum DetectSurface()
    {
        Vector3 capsuleColliderCenterInWorldSpace = capsuleColliderUtility.CapsuleColliderData.Collider.bounds.center;
        float raycastDistance = capsuleColliderUtility.SlopeData.DistanceGroundCheck;
        
        if (Physics.Raycast(capsuleColliderCenterInWorldSpace, Vector3.down, out RaycastHit hit, raycastDistance, groundLayer))
        {
            GroundType surface = hit.collider.GetComponent<GroundType>();
            currentSurface = surface ? surface.GetGroundType() : GroundTypeEnum.Rock;

            if (currentSurface != lastSurface)
            {
                // Surface changed → appliquer effets, sons, etc.
                lastSurface = currentSurface;
            }
        }
        else
        {
            currentSurface = GroundTypeEnum.None;
        }
        
        return currentSurface;
    }
}
