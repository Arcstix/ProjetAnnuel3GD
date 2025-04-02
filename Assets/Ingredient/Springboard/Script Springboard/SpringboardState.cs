using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpringboardState : MonoBehaviour
{
    public Rigidbody player;
    public abstract void Enter(GameObject gameObject);

    public abstract void Tick(GameObject gameObject);
    
    public abstract void Exit(GameObject gameObject);
    
    #region Detection du player
    private void OnTriggerEnter(Collider other)
    {
         if(other.CompareTag("Player"))
         {
             GetComponent<StateMachineSpringboard>().ChangeState(GetComponent<PushSpringboardState>());
             player = other.gameObject.GetComponentInChildren<Rigidbody>();
         }
    }
    #endregion 
}
