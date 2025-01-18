using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineDestructible : MonoBehaviour
{
    [SerializeField] private DestructibleState defaultState;
    public DestructibleState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = defaultState;
        currentState.Enter(gameObject);
    }
    public void ChangeState(DestructibleState newState)
    {
        if (currentState != null)
        {
            currentState.Exit(gameObject);
        }

        currentState = newState;
        currentState.Enter(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.Tick(gameObject);
        }
    }
}
