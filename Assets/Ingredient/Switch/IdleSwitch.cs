using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSwitch : SwitchState
{
    public Transform interactableObject;
    
    public override void Enter(GameObject refObject)
    {
        Debug.Log("je suis en idle");
    }
    public override void Tick(GameObject refObject)
    {
        
    }
    public override void Exit(GameObject refObject)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable") || other.CompareTag("Movable")) //contact avec le player
        {
            Debug.Log("détection de l'interactable");
            interactableObject = other.transform;
            Debug.Log("j'ai trouvé l'interactable" + interactableObject.name);
            GetComponent<StateMachineSwitch>().ChangeState(GetComponent<ActivationSwitch>());
        }
    }
}
