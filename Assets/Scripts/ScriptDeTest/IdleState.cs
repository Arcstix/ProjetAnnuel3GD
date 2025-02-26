using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : CapsuleState
{
    [SerializeField] private GameObject lightWhite;
    public override void Enter(GameObject gameObject)
    {
        lightWhite.SetActive(true);
    }
    public override void Tick(GameObject gameObject)
    {
        base.Tick(gameObject);
        if(detected)
        {
            GetComponent<StateMachineCapsule>().ChangeState(GetComponent<AlertState>());
        }
    }
    public override void Exit(GameObject gameObject)
    {
        lightWhite.SetActive(false);
    }
}
