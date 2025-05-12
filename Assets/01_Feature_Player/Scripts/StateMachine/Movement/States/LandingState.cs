using System;
using UnityEngine;

public class LandingState : AirState
{
    public event Action OnNoStaminaLanding;
    
    public LandingState(MovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    public void CheckChargeRemaining()
    {
        if (!metricsManager.HasRightCharge() && !metricsManager.HasLeftCharge())
        {
            OnNoStaminaLanding?.Invoke();
        }
    }
}
