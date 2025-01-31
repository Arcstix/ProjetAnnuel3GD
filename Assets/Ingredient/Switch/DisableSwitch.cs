using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSwitch : SwitchState
{
    public override void Enter(GameObject refObject)
    {
        Debug.Log("je suis désactiver");
        //arret de l'animation
    }
    public override void Tick(GameObject refObject)
    {
        GetComponent<StateMachineSwitch>().ChangeState(GetComponent<IdleSwitch>());
        //timer si nescéssaire selon l'animation
    }
    public override void Exit(GameObject refObject)
    {
        
    }
}
