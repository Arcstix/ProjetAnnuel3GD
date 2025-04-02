using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState : MonoBehaviour
{
    #region Variables visible
    [Header("Zone de détection")]
    [Tooltip("Range de la zone de détection")]
    public float detectionRange;
    [Tooltip("Angle de la zone de détection")]
    public float detectionAngle;
    [Header("Comportement Ennemis")]
    [Tooltip("Référence à la zone mobile de l'ennemi")]
    public Transform headTransform;
    #endregion
    
    #region Hidden Variables
    
    [HideInInspector] public UnityEngine.AI.NavMeshAgent _navMeshAgent;
    public Transform playerTransform;
    [HideInInspector] public LightManager lightManager;
    [HideInInspector] public bool isPatrolling = false;
    
    #endregion
    
   
    private void Awake()
    {
        isPatrolling = false;
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        lightManager = GetComponent<LightManager>();
        //headTransform = GameObject.FindWithTag("Mobile").transform;
    }


    public abstract void Enter(GameObject gameObject);

    public virtual void Tick(GameObject gameObject)
    {
        if (isPatrolling)
        {
            _navMeshAgent.speed = 5f;
        }
        if (!isPatrolling)
        {
            _navMeshAgent.speed = 0f;

        }
        if (PlayerIsDetected())
        {
            GetComponent<StateMachineEnemy>().ChangeState(GetComponent<AlertEnemyState>());
        }
    }
    
    public abstract void Exit(GameObject gameObject);

    #region Detection du player

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<StateMachineEnemy>().ChangeState(GetComponent<AlertEnemyState>());
        }
    }
    public bool PlayerIsDetected()
    {
        // Cible potentielle, probablement le joueur dans ce contexte
        Transform target = playerTransform; 

        // 1. V�rifiez si le joueur est dans la plage de d�tection
        if (Vector3.Distance(transform.position, target.position) <= detectionRange)
        {
            // 2. V�rifiez si le joueur est dans le bon angle
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) <= detectionAngle) 
            {
                // 3. Raycast pour v�rifier les obstacles
                RaycastHit hit;
                //Debug.DrawLine(transform.forward, directionToTarget, Color.red);
                if (Physics.Raycast(transform.position, directionToTarget, out hit, detectionRange))
                {
                    // 4. V�rifiez si le raycast a touch� le joueur sans obstacle entre les deux
                    if (hit.transform == target)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    #endregion 
}
