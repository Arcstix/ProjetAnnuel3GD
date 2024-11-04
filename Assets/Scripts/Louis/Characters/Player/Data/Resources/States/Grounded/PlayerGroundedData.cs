using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerGroundedData
{
    [field: SerializeField] [field: Range(0f, 25f)] public float BaseSpeed { get; private set; }
    [field: SerializeField] public float GravityMultiplier { get; private set; }
    [field: SerializeField] public AnimationCurve GravityModifier { get; private set; }
    [field: SerializeField] public AnimationCurve SlopeSpeedAngle { get; private set; }
    [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }
    [field: SerializeField] public PlayerWalkData WalkData { get; private set; }
    [field: SerializeField] public PlayerRunData RunData { get; private set; }
}
