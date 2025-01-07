using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnDestructible : DestructibleState
{
    [SerializeField] private float timer; 
    [SerializeField] private float timeToIdle;
    public override void Enter(GameObject gameObject)
    {
        //GetComponent<MeshRenderer>().enabled = true;
    }

    public override void Tick(GameObject gameObject)
    {
        //animation
        timer += Time.deltaTime;
        if (timer >= timeToIdle)
        {
            timer = 0; //timer à zéro
            GetComponent<StateMachineDestructible>().ChangeState(GetComponent<IdleDestructible>()); //changer d'état
        }
    }

    public override void Exit(GameObject gameObject)
    {
        GetComponent<MeshRenderer>().enabled = true;
    }
}
