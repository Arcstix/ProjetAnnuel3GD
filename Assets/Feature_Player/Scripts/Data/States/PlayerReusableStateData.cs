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
    public bool CanMove { get; set; }
    public Vector3 InstancePosition { get; set; }
    public bool RightInput { get; set; } = false;
    public bool LeftInput { get; set; } = false;
    public bool RightActivation { get; set; } = false;
    public bool LeftActivation { get; set; } = false;
    public GameObject ObjectAimed { get; set; }
    public GameObject RightParent { get; set; }
    public GameObject LeftParent { get; set; }
    public BallManager RightObject { get; set; }
    public BallManager LeftObject { get; set; }
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
