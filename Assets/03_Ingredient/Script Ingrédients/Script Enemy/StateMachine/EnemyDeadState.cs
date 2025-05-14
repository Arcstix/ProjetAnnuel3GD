using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeadState : EnemyState
{
    
    public UnityEvent onDeath;

    public event Action OnEnterDeath;
    
    public override void Enter()
    {
        onDeath.Invoke();
    }
    public override void Tick()
    {
        
    }
    public override void Exit()
    {
        
    }
}
