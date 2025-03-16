using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemyState : EnemyState
{
    #region Variables Visible
    [Header("Patrol")]
    [Tooltip("List des points de patrol")]
    [SerializeField] private List<Transform> checkPoints;
    [Tooltip("Temps de pause à chaque point")]
    [SerializeField] private float waitTimeAtPoint; 
    #endregion
    
    #region Hidden Variables
    
    [HideInInspector] [SerializeField] private int currentPatrolIndex = 0; 
    [HideInInspector] [SerializeField] private bool movingForward = true; 
    [HideInInspector] [SerializeField] private bool isWaiting = false;
    
    #endregion
    
    public override void Enter(GameObject gameObject)
    {
        isPatrolling = true;
        lightManager.EnableSpotLightWhite();
        //Debug.Log("PatrolState");
    }

    public override void Exit(GameObject gameObject)
    {
        isPatrolling = false;
    }

    public override void Tick(GameObject gameObject)
    {
        base.Tick(gameObject);
        if (!PlayerIsDetected())
        {
            Patrol();
        }
        if (PlayerIsDetected())
        {
            //Debug.Log("PlayerDetected");
            StopCoroutine(WaitAtPoint());
            GetComponent<StateMachineEnemy>().ChangeState(GetComponent<AlertEnemyState>());
        }
    }

    #region Patrol
    private void Patrol()
    {
        if (!isWaiting)
        {
            _navMeshAgent.SetDestination(checkPoints[currentPatrolIndex].position);

            // Vérifier si l'ennemi est arrivé à destination
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                // Attendre un moment à l'arrivée avant de passer au point suivant
                StartCoroutine(WaitAtPoint());
                
            }
        }
    }

    #region WaitAtPoint

    private IEnumerator WaitAtPoint()
    {
        isWaiting = true; // Activer la pause

        // Attendre un certain temps avant de passer au point suivant
        yield return new WaitForSeconds(waitTimeAtPoint); 

        // Reprendre le mouvement après la pause
        isWaiting = false; 

        // Incrémenter ou décrémenter l'index en fonction de la direction
        if (movingForward)
        {
            // Si on est au dernier point, inverser la direction
            if (currentPatrolIndex < checkPoints.Count - 1)
            {
                currentPatrolIndex++; // Passer au point suivant
            }
            else
            {
                movingForward = false; // Inverser la direction
                currentPatrolIndex--; // Revenir au point précédent
            }
        }
        else
        {
            // Si on est au premier point, inverser la direction
            if (currentPatrolIndex > 0)
            {
                currentPatrolIndex--; // Revenir au point précédent
            }
            else
            {
                movingForward = true; // Inverser la direction
                currentPatrolIndex++; // Passer au point suivant
            }
        }
    }

    #endregion
    #endregion
}
