using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GroundedData
{
    [field: SerializeField] [field: Range(0f, 25f)] public float BaseSpeed { get; private set; }
    [field: SerializeField] public AnimationCurve SlopeSpeedAngle { get; private set; }
    [field: SerializeField] public float TimeToReachTargetRotation { get; set; } = 0.2f;
    [field: SerializeField] public PlayerWalkData WalkData { get; private set; }
    [field: SerializeField] public PlayerRunData RunData { get; private set; }
}
