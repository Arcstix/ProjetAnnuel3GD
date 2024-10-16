using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStandbyData
{
    [field: SerializeField] public float MaxConnectionDistance { get; private set; }
    [field: SerializeField] [field: Range(0f, 25f)] public float TimeBeforeAbilityCancel { get; private set; } = 2f;
}
