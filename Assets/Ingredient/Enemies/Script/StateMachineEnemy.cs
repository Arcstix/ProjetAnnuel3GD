using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StateMachineEnemy : MonoBehaviour
{
    [Header("Spécifité Enemies")]
    public bool _flashLightOn;
    public bool patrol;
    public bool headMobile;
    
    #region Hidden Variables
    
    [HideInInspector] [SerializeField] private EnemyState defaultState;
    [HideInInspector] public EnemyState currentState;
    
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
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Tick(gameObject);
        }
    }
}
