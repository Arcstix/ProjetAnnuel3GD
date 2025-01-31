using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleDestructible : DestructibleState
{
    [SerializeField] private float speedDestructible;
    [SerializeField] private float speedAjout;

    public event Action<Vector3> DestructionEvent;
    
    public override void Enter(GameObject refObject)
    {
    }
    public override void Tick(GameObject refObject) //virtual si base.tick
    {
       
    }
    public override void Exit(GameObject refObject)
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Movable") || other.CompareTag("Interactable"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                DestructionEvent?.Invoke(rb.velocity.normalized);
                GetComponent<StateMachineDestructible>().ChangeState(GetComponent<DestroyDestructible>());
            }
        }
        
        if (other.CompareTag("Player")) //contact avec le player
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>(); //get le rigidbody

            if (playerRigidbody != null)
            {
                float playerSpeed = playerRigidbody.velocity.magnitude; // Vitesse du joueur

                // Vérifier si la vitesse est au-dessus ou en dessous du seuil
                if (playerSpeed >= speedDestructible)
                {
                    playerRigidbody.GetComponent<PlayerMetricsManager>().AddExternForce(speedAjout);
                    Debug.Log("vitesse ajouter" + playerRigidbody.velocity.magnitude);
                    DestructionEvent?.Invoke(playerRigidbody.velocity.normalized);
                    GetComponent<StateMachineDestructible>().ChangeState(GetComponent<DestroyDestructible>());
                }
            }
        }
    }
}
