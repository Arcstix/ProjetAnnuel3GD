using System;
using UnityEngine;

[Serializable]
public class FallingData 
{
    [field: SerializeField] public float SpeedModifier { get; private set; } = 1f;
    [field: SerializeField] public float GravityMultiplier { get; private set; } = 4f;
    [field: SerializeField] public AnimationCurve GravityModifier { get; private set; }
    [field: SerializeField] public float MaxFallingSpeed { get; private set; } = 52f;
}
