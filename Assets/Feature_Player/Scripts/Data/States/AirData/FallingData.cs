using System;
using UnityEngine;

[Serializable]
public class FallingData 
{
    [field: SerializeField] [field: Range(0f, 2f)] public float SpeedModifier { get; private set; } = 1f;

}
