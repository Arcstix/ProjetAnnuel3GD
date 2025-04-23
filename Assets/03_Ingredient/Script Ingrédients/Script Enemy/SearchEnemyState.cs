using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchEnemyState : EnemyState
{
    
    #region Hidden Variables

    [HideInInspector] public Vector3 PositionGoTo;
    
    #endregion
    
    public override void Enter()
    {
        Debug.Log("SearchState");
        _navMeshAgent.SetDestination(PositionGoTo);
    }

    public override void Exit()
    {
        
    }

    public override void Tick()
    {
        base.Tick();
        if (!PlayerIsDetected())
        {
            Search();
        }
        if (PlayerIsDetected())
        {
            GetComponent<StateMachineEnemy>().ChangeState(GetComponent<AlertEnemyState>());
        }
    }

    #region SearchFonction
    public void Search()
    {
        if (PositionGoTo != null)
        {
           isPatrolling = true;
            _navMeshAgent.SetDestination(PositionGoTo);
            
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                isPatrolling = false;
                GetComponent<StateMachineEnemy>().ChangeState(GetComponent<IdleEnemyState>());
            }
        }
    }
    #endregion
}
