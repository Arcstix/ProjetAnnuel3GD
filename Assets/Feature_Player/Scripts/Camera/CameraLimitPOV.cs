using System;
using UnityEngine;

[Serializable]
public class CameraLimitPOV
{
    [field: SerializeField] public float MinHorizontalValue { get; private set; } = -30f;
    [field: SerializeField] public float MaxHorizontalValue { get; private set; } = 30f;

}
