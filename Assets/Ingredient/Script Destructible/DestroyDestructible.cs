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
        
        // Désactiver tous les Colliders attachés à cet objet et à ses enfants
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        
        // Désactiver tous les MeshRenderers attachés à cet objet et à ses enfants
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }

        // Désactiver tous les Rigidbodies attachés à cet objet et à ses enfants
        Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true; // Rendre le Rigidbody kinematic
            rigidbody.detectCollisions = false; // Désactiver les collisions
        }

        Debug.Log("Tous les composants désactivés sur : " + gameObject.name);
    }

    public override void Tick(GameObject gameObject)
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

    public override void Exit(GameObject gameObject)
    {
    }
}
