using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerWalkData
{
    [field: SerializeField] public float SpeedModifier { get; private set; } = 0.225f;
}
