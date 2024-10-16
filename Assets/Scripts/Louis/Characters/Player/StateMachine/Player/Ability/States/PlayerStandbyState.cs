using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandbyState : PlayerAbilityState
{
    public PlayerStandbyState(PlayerAbilityStateMachine playerAbilityStateMachine) : base(playerAbilityStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if(_abilityData.ShootData.MaxTravelDistance > 100)
        {
            AddInputCallBack();
        }
        AddCancelInputCallBack();
        _playerAbilityStateMachine.ReusableStateData.CanUseAbility = true;
    }

    public override void Exit()
    {
        base.Exit();
        if (_abilityData.ShootData.MaxTravelDistance > 100)
        {
            RemoveInputCallBack();
        } 
        RemoveCancelInputCallBack();
    }
}
