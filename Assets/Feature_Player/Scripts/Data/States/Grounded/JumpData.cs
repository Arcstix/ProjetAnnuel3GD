using System;
using UnityEngine;

[Serializable]
public class JumpData 
{
    [field: SerializeField] [field: Range(0f, 1f)] public float SpeedModifier { get; private set; } = 0.5f;
    [field: SerializeField] public float JumpHeight { get; private set; } = 5f;
    [field: SerializeField] public float JumpTimer { get; private set; } = 0.5f;
    [field: SerializeField, Range(0, 5)] int maxAirJumps = 0;
}
