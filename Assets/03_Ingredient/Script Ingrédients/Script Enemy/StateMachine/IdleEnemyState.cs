using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

public class IdleEnemyState : EnemyState
{
    #region Variables visible
    private float idleTime = 0f;
    
    [Header("GoToPatrol")]
    [Tooltip("Temps que dure l'état idle avant de passer en Patrol")]
    [SerializeField] private float idleTimeBeforePatrol;

    private float timer;
    
    #endregion

    public event Action OnEnterIdle;
    public event Action OnExitIdle;
    
    public override void Enter()
    {
        isPatrolling = false;
        lightManager.EnableSpotLightWhite();
        //Debug.Log("IdleState");
        OnEnterIdle?.Invoke();
    }

    public override void Exit()
    {
        idleTime = 0f;
        if (stateMachine.HeadMovement != null)
        {
            stateMachine.HeadMovement.ResetValues();
        }
        OnExitIdle?.Invoke();
    }

    public override void Tick()
    {
        base.Tick();
        if (stateMachine.patrol)
        {
            timer += Time.deltaTime;
            if (timer >= idleTimeBeforePatrol)
            {
                timer = 0f;
                GetComponent<StateMachineEnemy>().ChangeState(GetComponent<PatrolEnemyState>());
            }
        }
        else if (stateMachine.headMobile)
        {
            stateMachine.HeadMovement.MoveHead();
        }

        if (stateMachine.flashLightOn)
        {
            lightManager.FlashLight();
        }
        if (PlayerIsDetected())
        {
            GetComponent<StateMachineEnemy>().ChangeState(GetComponent<AlertEnemyState>());
        }
    }
}
