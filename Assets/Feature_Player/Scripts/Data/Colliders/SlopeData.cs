using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlopeData 
{
    [field: SerializeField] [field: Range(0f, 0.25f)] public float StepHeightPercentage { get; private set; } = 0.125f;
    [field: SerializeField] [field: Range(0.2f, 3f)] public float DistanceGroundCheck { get; private set; } = 1.25f;
    [field: SerializeField] [field: Range(1f, 50f)] public float StepReachForce { get; private set; } = 20f;
}
