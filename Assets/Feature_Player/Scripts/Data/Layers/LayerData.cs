using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LayerData 
{
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }
}
