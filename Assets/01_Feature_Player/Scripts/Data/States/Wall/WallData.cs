using System;
using UnityEngine;

[Serializable]
public class WallData 
{
    [Header("Wallrunning")]
    [field: SerializeField] public LayerMask WallLayer;
    [field: SerializeField] public float WallRunForce;
    [field: SerializeField] public float Inclinaison = 5f;
    [field: SerializeField] [Range(0.01f,1)] public float SmoothInclinaison = 0.5f;

    [Header("Detection")]
    [field: SerializeField] public float WallCheckDistance;
    [field: SerializeField] public float MaxAngle;
}
