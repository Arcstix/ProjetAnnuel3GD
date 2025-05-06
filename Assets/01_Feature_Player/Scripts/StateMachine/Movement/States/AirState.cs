using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script utilis� quand le joueur est dans les airs
/// </summary>
public class AirState : MovementState
{
    //cr�er des raccourcies pour les variables
    protected CapsuleColliderUtility capsuleColliderUtility;
    protected GroundedData groundedData;
    protected JumpData jumpData;
    protected FallingData fallingData;
    protected float timer;
    protected Transform targetLandingPoint;
    
    protected Transform lastLandingPoint;

    public AirState(MovementStateMachine stateMachine) : base(stateMachine)
    {
        //faire le chemin ici comme �a = une fois
        capsuleColliderUtility = base.stateMachine.MovementManager.CapsuleUtility;
        groundedData = base.stateMachine.MovementManager.Metrics.CurrentMetrics.GroundedData;
        jumpData = base.stateMachine.MovementManager.Metrics.CurrentMetrics.JumpData;
        fallingData = base.stateMachine.MovementManager.Metrics.CurrentMetrics.FallingData;
    }

    public override void FixedTick()
    {
        base.FixedTick();

        if (!reusableData.OnTransportation && stateMachine.currentState != stateMachine.JumpState)
        {
            CheckDistanceToTheGround();
        }

        if (reusableData.HadJump && targetLandingPoint != null && targetLandingPoint != lastLandingPoint)
        {
            AdjustTrajectory();
        }
    }

    protected void CheckDistanceToTheGround()
    {
        Vector3 capsuleColliderCenterInWorldSpace = capsuleColliderUtility.CapsuleColliderData.Collider.bounds.center;

        Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, capsuleColliderUtility.SlopeData.DistanceGroundCheck, capsuleColliderUtility.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            reusableData.CanMove = true;
            reusableData.InAir = false;
        }
        else
        {
            reusableData.InAir = true;
        }
    }
    
    protected void DetectLandingZone()
    {
        Collider[] hits = Physics.OverlapSphere( rigidbody.transform.position + rigidbody.transform.forward * jumpData.DetectionRange * 0.5f, jumpData.DetectionRadius);
        float bestScore = Mathf.NegativeInfinity;
        Transform bestPlatform = null;

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<LandingPlatform>(out LandingPlatform landingPlatform))
            {
                if (landingPlatform.IsAvailable())
                {
                    Vector3 directionToPlatform = (hit.transform.position - rigidbody.transform.position).normalized;
                    float angle = Vector3.Angle(rigidbody.transform.forward, directionToPlatform);
                    float distance = Vector3.Distance(rigidbody.transform.position, hit.transform.position);

                    if (angle < jumpData.MaxDetectionAngle)
                    {
                        float score = Mathf.Cos(angle * Mathf.Deg2Rad) / (distance + 0.1f); // Priorité à l'angle, puis la distance
                    
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestPlatform = hit.transform;
                        }
                    }
                }
            }
        }

        targetLandingPoint = bestPlatform;
    }
    
    void AdjustTrajectory()
    {
        Vector3 directionToTarget = (targetLandingPoint.position - rigidbody.transform.position).normalized;
        float distanceToTarget = Vector3.Distance(rigidbody.transform.position, targetLandingPoint.position);
        
        // Réduire la correction à mesure qu'on approche de la plateforme
        float correctionMultiplier = Mathf.Clamp01(jumpData.DetectionRange/ distanceToTarget);
        Vector3 correction = new Vector3(directionToTarget.x, 0, directionToTarget.z) * jumpData.CorrectionForce * correctionMultiplier * Time.fixedDeltaTime;
        rigidbody.velocity += correction;
        
        // Réduire la vitesse horizontale si on est très proche
        if (distanceToTarget < jumpData.MagnetThreshold * 2)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x * jumpData.SpeedReductionFactor, rigidbody.velocity.y, rigidbody.velocity.z * jumpData.SpeedReductionFactor);
        }
    }
}
