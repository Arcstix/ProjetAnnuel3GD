using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerReusableStateData
{
    public Vector2 MovementInput { get; set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public float MovementOnSlopeSpeedModifier { get; set; } = 1f;
    public bool ShouldWalk { get; set; }
    public ProjectileManager RightProjectileRef { get; set; }
    public ProjectileManager LeftProjectileRef { get; set; }
    public bool CanUseRightAbility { get; set; }
    public bool CanUseLeftAbility { get; set; }
    public bool OnRightAttraction { get; set; }
    public bool OnLeftAttraction { get; set; }
    public bool TransportationMode { get; set; } = true;
    public bool CanMove { get; set; }
    public bool OnTransportation { get; set; } = false;
    public bool ShouldSlowDown { get; set; } = false;
    public float CurrentTargetRotation { get; set; }
    public float TimeToReachTargetRotation { get; set; }
    public float DampedTargetRotationPassedTime { get; set; }

    private float turnSmoothVelocity;
    public bool InAir { get; set; }

    public ref float TurnSmoothVelocity 
    {
        get
        {
            return ref turnSmoothVelocity;
        }
    }

}
