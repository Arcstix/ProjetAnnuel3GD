using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerRotationData
{
    [field: SerializeField] public float TargetRotationReachTime { get; private set; } = 0.1f;
}
