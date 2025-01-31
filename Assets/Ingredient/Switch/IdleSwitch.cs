using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSwitch : SwitchState
{
    public Transform interactableObject;
    public override void Enter(GameObject gameObject)
    {
        Debug.Log("je suis en idle");
    }
    public override void Tick(GameObject gameObject)
    {
        
    }
    public override void Exit(GameObject gameObject)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable")) //contact avec le player
        {
            Debug.Log("détection de l'interactable");
            interactableObject = other.transform;
            Debug.Log("j'ai trouvé l'interactable" + interactableObject.name);
            GetComponent<StateMachineSwitch>().ChangeState(GetComponent<ActivationSwitch>());
        }
    }
}
