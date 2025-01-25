using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleDestructible : DestructibleState
{
    [SerializeField] private float speedDestructible;
    [SerializeField] private float speedAjout;

    public event Action<Vector3> DestructionEvent;
    
    public override void Enter(GameObject gameObject)
    {
        Debug.Log("de retour en idle");
    }
    public override void Tick(GameObject gameObject) //virtual si base.tick
    {
       
    }
    public override void Exit(GameObject gameObject)
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player")) //contact avec le player
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>(); //get le rigidbody

            if (playerRigidbody != null)
            {
                float playerSpeed = playerRigidbody.velocity.magnitude; // Vitesse du joueur

                // Afficher la vitesse dans la console
                Debug.Log($"Vitesse du joueur au moment du contact : {playerSpeed} m/s");

                // Vérifier si la vitesse est au-dessus ou en dessous du seuil
                if (playerSpeed >= speedDestructible)
                {
                    playerRigidbody.AddForce(playerRigidbody.velocity.normalized * speedAjout, ForceMode.Impulse);
                    Debug.Log("vitesse ajouter" + playerRigidbody.velocity.magnitude);
                    DestructionEvent?.Invoke(playerRigidbody.velocity.normalized);
                    GetComponent<StateMachineDestructible>().ChangeState(GetComponent<DestroyDestructible>());
                }
                else if (playerSpeed <= -speedDestructible)
                {
                    Debug.Log("Le joueur manque de vitesse");
                }
               
            }
            else
            {
                Debug.LogWarning("Le joueur détecté n'a pas de Rigidbody attaché.");
            }
        }
    }
}
