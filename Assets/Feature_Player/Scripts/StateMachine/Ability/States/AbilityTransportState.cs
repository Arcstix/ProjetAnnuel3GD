using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTransportState : AbilityState
{
    public AbilityTransportState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (reusableData.LeftProjectile == null)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }

        if (reusableData.RightProjectile == null)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }
    }

    #region Transport Methods
    
    protected void MoveToProjectile()
    {
        Vector3 direction = new Vector3();

        // if (_stateMachine.IsRight)
        // {
        //     direction = (reusableData.RightProjectileRef.transform.position - rigidbody.worldCenterOfMass).normalized;
        // }
        // else
        // {
        //     direction = (reusableData.LeftProjectileRef.transform.position - rigidbody.worldCenterOfMass).normalized;
        // }
        //
        // rigidbody.AddForce(direction * metricsManager.CurrentPlayerSO.AbilityData.TransportationData.BaseSpeed - rigidbody.velocity, ForceMode.VelocityChange);
    }

    protected void LeftMoveToRight()
    {
        //Vector3 direction = (reusableData.RightProjectileRef.transform.position - reusableData.LeftProjectileRef.transform.position).normalized;

        //reusableData.LeftProjectileRef.MoveToDirection(direction, reusableData.RightProjectileRef.transform.position, reusableData.LeftProjectileRef.transform.position);
    }

    protected void RightMoveToLeft()
    {
        //Vector3 direction = (reusableData.LeftProjectileRef.transform.position - reusableData.RightProjectileRef.transform.position).normalized;

        //reusableData.RightProjectileRef.MoveToDirection(direction, reusableData.LeftProjectileRef.transform.position, reusableData.RightProjectileRef.transform.position);
    }
    
    #endregion
}
