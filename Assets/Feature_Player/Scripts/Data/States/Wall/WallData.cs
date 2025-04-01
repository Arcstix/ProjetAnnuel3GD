using System;
using UnityEngine;

[Serializable]
public class WallData 
{
    [Header("Wallrunning")]
    [field: SerializeField] public LayerMask WallLayer;
    [field: SerializeField] public float WallRunForce;
    [field: SerializeField] public float WallClimbSpeed;
    [field: SerializeField] public float MaxWallRunTime;

    [Header("Detection")]
    [field: SerializeField] public float WallCheckDistance;
    [field: SerializeField] public float MinJumpHeight;
    [field: SerializeField] public float WallCheckRadius;
}
