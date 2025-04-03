using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSpringboardState : SpringboardState
{
    [SerializeField] private float timerOut;
    
    public override void Enter(GameObject gameObject)
    {
        
    }
    public override void Tick(GameObject gameObject)
    {
        
    }
    public override void Exit(GameObject gameObject)
    {
       
    }
    #region Detection du player
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.gameObject.GetComponentInChildren<Rigidbody>();
            GetComponent<PushSpringboardState>().player= player; 
            GetComponent<StateMachineSpringboard>().ChangeState(GetComponent<PushSpringboardState>());
        }
    }
    #endregion 
}
