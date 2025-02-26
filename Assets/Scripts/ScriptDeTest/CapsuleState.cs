using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CapsuleState : MonoBehaviour
{
   public bool detected = false;

    public abstract void Enter(GameObject gameObject);

    public virtual void Tick(GameObject gameObject)
    {
        if(detected)
        {
            GetComponent<StateMachineCapsule>().ChangeState(GetComponent<AlertState>());
        }
        if(detected == false)
        {
            GetComponent<StateMachineCapsule>().ChangeState(GetComponent<IdleState>());
        }
    }
    public abstract void Exit(GameObject gameObject);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si l'objet entrant est le joueur
        {
            Debug.Log("Joueur détecté !");
            detected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si l'objet sortant est le joueur
        {
            Debug.Log("Joueur sorti !");
            detected = false;
        }
    }

}
