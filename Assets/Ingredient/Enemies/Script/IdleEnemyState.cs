using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

public class IdleEnemyState : EnemyState
{
    #region Variables visible
    [Header("MoveHead")]
    [Tooltip("Vitesse de rotation de la tête")]
    [SerializeField] private float EnemyRotationSpeed; 
    [Tooltip("Angle maximal de rotation")]
    [SerializeField] private float EnemyRotationAngle;
    private float idleTime = 0f;
    
    [Header("GoToPatrol")]
    [Tooltip("Temps que dure l'état idle avant de passer en Patrol")]
    [SerializeField] private float idleTimeBeforePatrol;
    private float timer = 0f;
    #endregion
    
    public override void Enter(GameObject gameObject)
    {
        isPatrolling = false;
        lightManager.EnableSpotLightWhite();
        //Debug.Log("IdleState");
    }

    public override void Exit(GameObject gameObject)
    {
        idleTime = 0f;
    }

    public override void Tick(GameObject gameObject)
    {
        base.Tick(gameObject);
        if (GetComponent<StateMachineEnemy>().patrol)
        {
            timer += Time.deltaTime;
            if (timer >= idleTimeBeforePatrol)
            {
                timer = 0f;
                GetComponent<StateMachineEnemy>().ChangeState(GetComponent<PatrolEnemyState>());
            }
        }
        else if (GetComponent<StateMachineEnemy>().headMobile)
        {
            MobileHead();
        }
        
        if (PlayerIsDetected())
        {
            GetComponent<StateMachineEnemy>().ChangeState(GetComponent<AlertEnemyState>());
        }
    }

  
    #region HeadMobile
    // la tête de l'ennemi tourne de gauche à droite
    private void MobileHead()
    {
        if (headTransform == null) return; // Vérification de la tête

        idleTime += Time.deltaTime; // Incrémente le temps écoulé

        float rotationY = Mathf.Sin(idleTime * EnemyRotationSpeed * Mathf.Deg2Rad) * EnemyRotationAngle;
        headTransform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }
    #endregion
}
