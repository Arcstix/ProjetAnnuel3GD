using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : CapsuleState
{
    [SerializeField] private GameObject lightRed;
    public override void Enter(GameObject gameObject)
    {
        lightRed.SetActive(true);
    }
    public override void Tick(GameObject gameObject)
    {
        base.Tick(gameObject);
        if(detected == false)
        {
            GetComponent<StateMachineCapsule>().ChangeState(GetComponent<IdleState>());
        }
    }
    public override void Exit(GameObject gameObject)
    {
        lightRed.SetActive(false);
    }
}
