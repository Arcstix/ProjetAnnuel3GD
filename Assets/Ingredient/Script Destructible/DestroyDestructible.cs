using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDestructible : DestructibleState
{
    [SerializeField] private float timer;
    [SerializeField] private float timeToRespawn;

    public event Action OnDestroyed;
    
    public override void Enter(GameObject refObject)
    {
        Debug.Log("EnterInDestroyDestructible");
        
        OnDestroyed?.Invoke();
        
        // Désactiver tous les Colliders attachés à cet objet et à ses enfants
        Collider[] colliders = refObject.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
        
        // Désactiver tous les MeshRenderers attachés à cet objet et à ses enfants
        MeshRenderer[] meshRenderers = refObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }

        // Désactiver tous les Rigidbodies attachés à cet objet et à ses enfants
        Rigidbody[] rigidbodies = refObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true; // Rendre le Rigidbody kinematic
            rb.detectCollisions = false; // Désactiver les collisions
        }

        Debug.Log("Tous les composants désactivés sur : " + refObject.name);
    }

    public override void Tick(GameObject refObject)
    {
        
        // Timer avant la réactivation
        timer += Time.deltaTime;
        //Debug.Log("timer lancer" + timer);
        if (timer >= timeToRespawn)
        {
            timer = 0; // Réinitialise le timer
            GetComponent<StateMachineDestructible>().ChangeState(GetComponent<RespawnDestructible>()); // Change l'état
        }
    }

    public override void Exit(GameObject refObject)
    {
    }
}
