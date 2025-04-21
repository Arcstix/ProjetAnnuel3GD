using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StateMachineEnemy : MonoBehaviour
{
    [Header("Spécifité Enemies")]
    [Tooltip("Si l'ennemis se déplace")]
    public bool patrol;
    [Tooltip("Si ça tete est mobile")]
    public bool headMobile;
    [Tooltip("Si ça lumière clignote")]
    public bool flashLightOn;
    
    #region Hidden Variables
    
    [SerializeField] private EnemyState defaultState;
     public EnemyState currentState;
    
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = defaultState;
        currentState.Enter(gameObject);
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit(gameObject);
        }

        currentState = newState;
        currentState.Enter(gameObject);
        Debug.Log(currentState);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Tick(gameObject);
        }
    }
}
