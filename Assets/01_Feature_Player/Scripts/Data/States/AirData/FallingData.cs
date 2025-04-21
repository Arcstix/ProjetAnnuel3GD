using System;
using UnityEngine;

[Serializable]
public class FallingData 
{
    [field: SerializeField] public float SpeedModifier { get; private set; } = 1f;
    [field: SerializeField] public float GravityModifier { get; private set; } = 4f;
    [field: SerializeField] public float MaxFallingSpeed { get; private set; } = 52f;
}
