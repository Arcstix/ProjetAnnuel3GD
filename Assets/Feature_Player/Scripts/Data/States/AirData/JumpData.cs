using System;
using UnityEngine;

[Serializable]
public class JumpData 
{
    [field: SerializeField] public float SpeedModifier { get; private set; } = 0.5f;
    [field: SerializeField] public float JumpHeight { get; private set; } = 5f;
    [field: SerializeField] public float JumpTimer { get; private set; } = 0.5f;
    [field: SerializeField, Range(0, 5)] public int MaxAirJumps = 0;
    
    [Header("Assistance Settings")]
    [Tooltip("Distance max pour détecter une plateforme")]
    [field: SerializeField] public float DetectionRange = 5f;
    [Tooltip("Force d'ajustement de la trajectoire")]
    [field: SerializeField] public float CorrectionForce = 2f;
    [Tooltip("Distance max pour aimanter à l'atterrissage")]
    [field: SerializeField] public float MagnetThreshold = 1f;
    [Tooltip("Rayon pour OverlapSphere")]
    [field: SerializeField] public float DetectionRadius = 2f;
    [Tooltip("Angle max pour considérer une plateforme en face du joueur")]
    [field: SerializeField] public float MaxDetectionAngle = 45f;
    [Tooltip("Facteur de réduction de vitesse proche de la plateforme")]
    [field: SerializeField] public float SpeedReductionFactor = 0.5f;
}
