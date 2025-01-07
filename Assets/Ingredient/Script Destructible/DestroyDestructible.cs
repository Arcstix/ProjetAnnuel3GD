using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDestructible : DestructibleState
{
    [SerializeField] private float timer; 
    [SerializeField] private float timeToRespawn;
    public override void Enter(GameObject gameObject)
    {
        Debug.Log("EnterInDestroyDestructible");
        //désactivation du mesh
        GetComponent<MeshRenderer>().enabled = false;
    }

    public override void Tick(GameObject gameObject)
    {
      //timer avant la réactivaiton
      timer += Time.deltaTime;
      if (timer >= timeToRespawn)
      {
          timer = 0; //timer à zéro
          GetComponent<StateMachineDestructible>().ChangeState(GetComponent<RespawnDestructible>()); //changer d'état
      }
    }

    public override void Exit(GameObject gameObject)
    {
       
    }
}
