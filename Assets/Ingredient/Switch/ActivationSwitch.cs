using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationSwitch : SwitchState
{
    [SerializeField] private Transform switchCenter; // Le point central de l'interrupteur
    [HideInInspector]
    public IdleSwitch idleSwitch;
    [SerializeField] private bool inCenter;

    public override void Enter(GameObject gameObject)
    {
        Debug.Log("je suis activer");
        idleSwitch = GetComponent<IdleSwitch>();
        inCenter = false;
    }
    public override void Tick(GameObject gameObject)
    {
        if (inCenter == false)
        {
            TargetInCenter();
        }

        if (idleSwitch.interactableObject.position == switchCenter.position && idleSwitch.interactableObject.rotation == switchCenter.rotation)
        {
            inCenter = true;
        }
         
        //animation du switch
        //idleSwitch.interactableObject.GetComponent<BoxCollider>().enabled = false;
    }
    public override void Exit(GameObject gameObject)
    {
        inCenter = false;
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Interactable")) //contact avec le player
        {
            // Réactiver le Rigidbody
            //idleSwitch.interactableObject.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("il n'y a plus d'interactable");
            GetComponent<StateMachineSwitch>().ChangeState(GetComponent<DisableSwitch>());
        }
    }

    private void TargetInCenter()
    {
        // Désactiver le Rigidbody
        // Positionne l'objet au centre
        idleSwitch.interactableObject.position = switchCenter.position;

        // Si nécessaire, ajustez la rotation
        idleSwitch.interactableObject.rotation = switchCenter.rotation;
        //idleSwitch.interactableObject = null;
    }
}
