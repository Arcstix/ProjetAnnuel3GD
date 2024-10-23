using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerTransportationData 
{
    [field: SerializeField] public float BaseSpeed { get; private set; } = 1f;
    [field: SerializeField] public AnimationCurve SpeedModifier { get; private set; }
}
