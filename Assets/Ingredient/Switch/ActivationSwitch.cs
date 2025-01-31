using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivationSwitch : SwitchState
{
    [SerializeField] private Transform switchCenter; // Le point central de l'interrupteur
    public IdleSwitch idleSwitch;
    [SerializeField] private bool inCenter;
    
    public UnityEvent onSwitch;
    public event Action onActivate;
    
    public override void Enter(GameObject refObject)
    {
        Debug.Log("je suis activer");
        inCenter = false;
        idleSwitch.interactableObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        onSwitch?.Invoke();
        onActivate?.Invoke();
    }
    public override void Tick(GameObject refObject)
    {
        if (inCenter == false)
        {
            TargetInCenter();
        }

        if (Vector3.Distance(idleSwitch.interactableObject.position, switchCenter.position) < 0.2f && Quaternion.Angle(idleSwitch.interactableObject.rotation, switchCenter.rotation) <= 0.2f)
        {
            inCenter = true;
        }
         
        //animation du switch
        //idleSwitch.interactableObject.GetComponent<BoxCollider>().enabled = false;
    }
    public override void Exit(GameObject refObject)
    {
        inCenter = false;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == idleSwitch.interactableObject.gameObject) //contact avec le player
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
        idleSwitch.interactableObject.position = Vector3.Lerp(idleSwitch.interactableObject.position, switchCenter.position, 0.5f);

        // Si nécessaire, ajustez la rotation
        idleSwitch.interactableObject.rotation = Quaternion.Lerp(idleSwitch.interactableObject.rotation, switchCenter.rotation, 0.5f);
        //idleSwitch.interactableObject = null;
    }
}
