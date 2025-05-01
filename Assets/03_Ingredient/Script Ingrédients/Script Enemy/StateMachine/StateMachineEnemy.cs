using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StateMachineEnemy : MonoBehaviour
{
    #region Variables visible
    
    [Header("Spécifité Enemies")]
    [Tooltip("Si l'ennemis se déplace")]
    public bool patrol;
    [Tooltip("Si ça tete est mobile")]
    public bool headMobile;
    [Tooltip("Si ça lumière clignote")]
    public bool flashLightOn;
    
    [Header("Zone de détection")]
    [Tooltip("Range de la zone de détection")]
    public float detectionRange;
    [Tooltip("Angle de la zone de détection")]
    public float detectionAngle;
    
    [Header("Référence Mesh Ennemis")]
    [Tooltip("Référence à la zone mobile de l'ennemi")]
    public Transform headTransform;
    
    [Header("State Machine Information")]
    [SerializeField] private EnemyState defaultState;
    public EnemyState currentState;
    public bool isDead = false;
    
    [Space(10)]
    
    [Header("Gizmos Variables")]
    [Tooltip("Field Of View Line")]
    public int numberOfLine = 20;
    
    #endregion

    public HeadBehaviour HeadMovement { get; private set; }

    private void Awake()
    {
        if (headMobile)
        {
            HeadMovement = GetComponent<HeadBehaviour>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(defaultState);
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Tick();
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.FixedTick();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.yellow;

        if (headTransform != null)
        {
            Vector3 origin = headTransform.position;
            Vector3 forward = headTransform.forward;
            
            // Dessin du cône en lignes (rayons)
            float halfAngle = detectionAngle * 0.5f;

            for (int i = 0; i <= numberOfLine; i++)
            {
                float t = i / (float)numberOfLine;
                float angle = Mathf.Lerp(-halfAngle, halfAngle, t);
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                Vector3 direction = rotation * forward;
                Gizmos.DrawLine(origin, origin + direction * detectionRange);
            }
        }
    }
}
