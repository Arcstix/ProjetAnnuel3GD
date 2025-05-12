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
    public bool HadJump {get; set;} = false;
    public int NumberOfJump { get; set; } = 0;
    public int NumberOfConsecutiveTransportation { get; set; } = 0;
    public Vector3 InstancePosition { get; set; }
    public bool RightInput { get; set; } = false;
    public bool LeftInput { get; set; } = false;
    public bool RightActivation { get; set; } = false;
    public bool LeftActivation { get; set; } = false;
    public bool RightThrow { get; set; } = false;
    public bool LeftThrow { get; set; } = false;
    public InteractiveTarget ObjectAutoAimed { get; set; }
    public GameObject ObjectAimed { get; set; }
    public GameObject RightParent { get; set; }
    public GameObject LeftParent { get; set; }
    public ToolManager RightObject { get; set; }
    public ToolManager LeftObject { get; set; }
    public bool OnTransportation { get; set; } = false;
    public bool IsWallRunning { get; set; } = false;
    public bool ShouldSlowDown { get; set; } = false;
    public bool WallLeft { get; set; } = false;
    public bool WallRight { get; set; } = false;
    public GameObject OnWall { get; set; }
    public float CurrentTargetRotation { get; set; }
    public float DampedTargetRotationPassedTime { get; set; }
    
    public Vector3 TargetPosition { get; set; }

    private float turnSmoothVelocity;
    public bool InAir { get; set; }

    public ref float TurnSmoothVelocity => ref turnSmoothVelocity;
    public bool OnLandingPlatform { get; set; } = false;
}
