using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAbilityData
{
    [field: SerializeField] public PlayerShootData ShootData { get; private set; }
    [field: SerializeField] public PlayerStandbyData StandbyData { get; private set; }
    [field: SerializeField] public PlayerTransportationData TransportationData { get; private set; }
    [field: SerializeField] public PlayerCancelAbilityData CancelAbilityData { get; private set; }
}
