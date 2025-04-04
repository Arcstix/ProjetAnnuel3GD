using System;
using UnityEngine;

[Serializable]
public class WallData 
{
    [Header("Wallrunning")]
    [field: SerializeField] public LayerMask WallLayer;
    [field: SerializeField] public float WallRunForce;

    [Header("Detection")]
    [field: SerializeField] public float WallCheckDistance;
    [field: SerializeField] public float MaxAngle;
}
