using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerShootData 
{
    [field: SerializeField] public ProjectileManager ProjectilePrefab { get; private set; }
    [field: SerializeField] public float BaseSpeed { get; private set; } = 1f;
    [field: SerializeField] public AnimationCurve SpeedModifier { get; private set; }

    [field: SerializeField] public float MaxTravelDistance { get; private set; } = 10f;
}
