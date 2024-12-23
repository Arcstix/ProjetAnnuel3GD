using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityStateMachine : StateMachine
{
    public PlayerAbilityManager AbilityManager { get; }

    public PlayerReusableStateData ReusableStateData { get; }
    
    public PlayerAbilityStateMachine(PlayerAbilityManager playerStateMachine, bool isRight)
    {
        AbilityManager = playerStateMachine;
        ReusableStateData = playerStateMachine.ReusableData;
    }
}
