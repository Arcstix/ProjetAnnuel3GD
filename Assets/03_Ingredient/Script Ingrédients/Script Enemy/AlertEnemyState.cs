using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertEnemyState : EnemyState
{
    #region Variables Visibles
    [Header("AlertState")]
    [Tooltip("Vitesse à laquelle il suit le joueur du regard")]
    [SerializeField] private float rotationSpeed;
    #endregion
    
    #region Hidden Variables

    [HideInInspector] public Vector3 lastSeenPosition;
    
    #endregion
    
    public override void Enter()
    {
        healthPlayer = playerTransform.GetComponent<HealthPlayer>();
        healthPlayer.enemyAlerted = true;
        
        isPatrolling = false;
        if (_navMeshAgent)
        {
            _navMeshAgent.speed = 0f;
        }
        
        lightManager.EnableSpotLightRed();
        //Debug.Log("AlertState");
    }

    public override void Exit()
    {
        GetComponent<SearchEnemyState>().PositionGoTo = lastSeenPosition;
        healthPlayer.enemyAlerted = false;
    }

    public override void Tick()
    {
        base.Tick();
        //fonction damage
        if (PlayerIsDetected())
        {
            LookAtPlayer();
        }
        if (!PlayerIsDetected())
        {
            CapturePlayerPosition();
            if (GetComponent<StateMachineEnemy>().patrol)
            {
                 PatrolTrue();
            }
            else
            {
                 PatrolFalse();
            }
        }
    }

    #region Idle or Search
    public void PatrolTrue()
    {
        GetComponent<StateMachineEnemy>().ChangeState(GetComponent<SearchEnemyState>());
    }
    public void PatrolFalse()
    {
        GetComponent<StateMachineEnemy>().ChangeState(GetComponent<IdleEnemyState>());
    }
    #endregion

    #region LookPlayer
    private void LookAtPlayer()
    {
        // Calculer la direction vers le joueur
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0;
        
        // Calculer la rotation nécessaire pour regarder le joueur
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Appliquer la rotation à l'ennemi (en douceur ou instantanément)
        stateMachine.headTransform.rotation = Quaternion.Slerp(stateMachine.headTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    #endregion

    #region Position Player For Search

    void CapturePlayerPosition()
    {
        lastSeenPosition = playerTransform.position;
        Debug.Log("Position du joueur capturée à : " + lastSeenPosition);
    }

    #endregion 
}
