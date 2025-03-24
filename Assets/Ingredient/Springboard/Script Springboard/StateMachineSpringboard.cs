using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineSpringboard : MonoBehaviour
{
    #region Hidden Variables
    
     [SerializeField] private SpringboardState defaultState;
    public SpringboardState currentState;
    
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = defaultState;
        currentState.Enter(gameObject);
    }

    public void ChangeState(SpringboardState newState)
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
