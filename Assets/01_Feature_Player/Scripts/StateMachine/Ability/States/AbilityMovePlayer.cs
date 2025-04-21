using System;
using UnityEngine;

public class AbilityMovePlayer : AbilityTransportState
{
    private Vector3 refVelocity;
    private Vector3 startPosition;
    
    public event Action<float> SpeedModifierEvent;

    public event Action EnterDash;
    public event Action ExitDash;
    
    public AbilityMovePlayer(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        playerInteraction = _stateMachine.AbilityManager.GetComponent<PlayerInteraction>();

        if (reusableData.RightObject)
        {
            reusableData.RightObject.GetComponent<InteractionSystem>().Interactable(true);
        }
        else
        {
            reusableData.LeftObject.GetComponent<InteractionSystem>().Interactable(true);
        }
        
        EnterDash?.Invoke();
        reusableData.OnTransportation = true;
        startPosition = _stateMachine.AbilityManager.transform.position;
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
        MovePlayer();
    }

    public override void Exit()
    {
        base.Exit();
        playerInteraction.Interactable(false);
        ExitDash?.Invoke();
        reusableData.OnTransportation = false;
    }

    private void MovePlayer()
    {
        var rb = _stateMachine.AbilityManager.Rb;
        
        if (reusableData.RightObject != null)
        {
            playerInteraction.Interactable(true);
            
            // Move Player to RightObject
            Vector3 direction = (reusableData.RightObject.transform.position -
                                  _stateMachine.AbilityManager.transform.position).normalized;
            

            float currentSpeed = CalculateSpeed(reusableData.RightObject.transform.position);
            
            rb.velocity = direction * (currentSpeed * metricsManager.ExternForce);
            
            Debug.DrawRay(rb.position, rb.velocity, Color.green);
            Debug.DrawRay(rb.position, direction * 4f, Color.blue);
        }
        else
        {
            playerInteraction.Interactable(true);
            
            // Move Player to LeftObject
            Vector3 direction = (reusableData.LeftObject.transform.position -
                                 _stateMachine.AbilityManager.transform.position).normalized;

            float currentSpeed = CalculateSpeed(reusableData.LeftObject.transform.position);
            
            rb.velocity = direction * (currentSpeed * metricsManager.ExternForce);
        }
    }
    
    private float CalculateSpeed(Vector3 targetPosition)
    {
        float totalDistance = Vector3.Distance(startPosition, targetPosition);
        
        float currentDistance = Vector3.Distance(startPosition, _stateMachine.AbilityManager.transform.position);
        
        float currentPercentageDistance = currentDistance / totalDistance;

        float currentSpeedMultiplier =
            metricsManager.CurrentMetrics.AbilityData.TransportCurve.Evaluate(currentPercentageDistance);
        
        SpeedModifierEvent?.Invoke(currentPercentageDistance);
        
        if (currentSpeedMultiplier < 0.1f)
        {
            currentSpeedMultiplier = 0.1f;
        }
        
        return  currentSpeedMultiplier * metricsManager.CurrentMetrics.AbilityData.TransportPlayerSpeed;
    }
}
