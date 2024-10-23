using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    private AIStateMachine aiStateMachine;

    [field: SerializeField] public int AbilityID { get; private set; } = 1;
    [field: SerializeField] public float Durability { get; private set; } = 30f;

    private void Awake()
    {
        aiStateMachine = new AIStateMachine(this);
    }

    private void Start()
    {
        aiStateMachine.ChangeState(aiStateMachine.AliveState);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(aiStateMachine.currentState != aiStateMachine.DeadState)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerAbilityManager abilityManager = collision.gameObject.GetComponent<PlayerAbilityManager>();
                if (abilityManager.playerAbilityStateMachine.currentState == abilityManager.playerAbilityStateMachine.TransportationState)
                {
                    aiStateMachine.ChangeState(aiStateMachine.DeadState);
                    abilityManager.GetComponent<PlayerMetricsManager>().AddDurability(AbilityID, Durability);
                }
            }
        }        
    }
}
