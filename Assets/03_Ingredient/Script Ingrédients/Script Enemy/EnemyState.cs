using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState : MonoBehaviour
{
    #region Hidden Variables
    
    protected NavMeshAgent _navMeshAgent;
    protected LightManager lightManager;
    protected bool isPatrolling = false;
    protected HealthPlayer healthPlayer;
    protected Transform playerTransform;
    protected StateMachineEnemy stateMachine;
    
    #endregion
    
   
    private void Awake()
    {
        isPatrolling = false;
        stateMachine = GetComponent<StateMachineEnemy>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        healthPlayer = playerTransform.GetComponent<HealthPlayer>();
        lightManager = GetComponent<LightManager>();
    }


    public virtual void Enter() {}

    public virtual void Tick()
    {
        if (_navMeshAgent)
        {
            if (isPatrolling)
            {
                _navMeshAgent.speed = 5f;
            }
            if (!isPatrolling)
            {
                _navMeshAgent.speed = 0f;
            }
        }
    }
    
    public virtual void FixedTick()
    {
        // Use Physics for Raycasting so we are in FixedUpdate
        if (PlayerIsDetected() && stateMachine.currentState != GetComponent<AlertEnemyState>())
        {
            GetComponent<StateMachineEnemy>().ChangeState(GetComponent<AlertEnemyState>());
        }
    }
    
    public virtual void Exit() {}

    #region Detection du player
    protected bool PlayerIsDetected()
    {
        // 1. V�rifiez si le joueur est dans la plage de d�tection
        if (Vector3.Distance(transform.position, playerTransform.position) <= stateMachine.detectionRange)
        {
            // 2. V�rifiez si le joueur est dans le bon angle par rapport à la tête de l'ennemie
            Vector3 directionToPlayer = (playerTransform.position - stateMachine.headTransform.position).normalized;
            if (Vector3.Angle(stateMachine.headTransform.forward, directionToPlayer) <= stateMachine.detectionAngle) 
            {
                // 3. Raycast pour v�rifier les obstacles
                RaycastHit hit;
                
                if (Physics.Raycast(stateMachine.headTransform.position, directionToPlayer, out hit, stateMachine.detectionRange, ~LayerMask.GetMask("Enemy")))
                {
                    // 4. V�rifiez si le raycast a touch� le joueur sans obstacle entre les deux
                    if (hit.transform == playerTransform)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    #endregion 
    
    #region Detection du playerMort
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Projectile"))
        {
            gameObject.SetActive(false);
        }
    }
    #endregion
}
