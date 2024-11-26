using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerShootData 
{
    [field: SerializeField] public ProjectileManager ProjectileRightPrefab { get; private set; }
    [field: SerializeField] public ProjectileManager ProjectileLeftPrefab { get; private set; }
    [field: SerializeField] public float BaseSpeed { get; private set; } = 1f;
    [field: SerializeField] public AnimationCurve SpeedModifier { get; private set; }
    [field: SerializeField] public bool UseAim { get; private set; } = false;
    [field: SerializeField] public float MaxTravelDistance { get; private set; } = 10f;
}
