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
    [Header("MoveHead")]
    [Tooltip("Vitesse de rotation de la tête")]
    [SerializeField] private float EnemyRotationSpeed; 
    [Tooltip("Angle maximal de rotation")]
    [SerializeField] private float EnemyRotationAngle;
    private float idleTime = 0f;
    #endregion
    
    #region Hidden Variables
    
    [HideInInspector] [SerializeField] private int currentPatrolIndex = 0; 
    [HideInInspector] [SerializeField] private bool movingForward = true; 
    [HideInInspector] [SerializeField] private bool isWaiting = false;
    
    #endregion
    
    public override void Enter()
    {
        isPatrolling = true;
        lightManager.EnableSpotLightWhite();
        //Debug.Log("PatrolState");
    }

    public override void Exit()
    {
        isPatrolling = false;
    }

    public override void Tick()
    {
        base.Tick();
        if (!PlayerIsDetected())
        {
            Patrol();
            if (GetComponent<StateMachineEnemy>().headMobile)
            {
                MobileHead();
            }
            if (GetComponent<StateMachineEnemy>().flashLightOn)
            {
                lightManager.FlashLight();
            }
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
#endregion

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
    
    #region HeadMobile
    // la tête de l'ennemi tourne de gauche à droite
    private void MobileHead()
    {
        if (stateMachine.headTransform == null) return; // Vérification de la tête

        idleTime += Time.deltaTime; // Incrémente le temps écoulé

        float rotationY = Mathf.Sin(idleTime * EnemyRotationSpeed * Mathf.Deg2Rad) * EnemyRotationAngle;
        stateMachine.headTransform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }
    #endregion
}
