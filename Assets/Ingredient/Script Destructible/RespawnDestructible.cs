using UnityEngine;

public class RespawnDestructible : DestructibleState
{
    public override void Enter(GameObject refObject)
    {
        Debug.Log("Cube respawn");
        
        // Désactiver tous les Colliders attachés à cet objet et à ses enfants
        Collider[] colliders = refObject.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
        
        // Désactiver tous les MeshRenderers attachés à cet objet et à ses enfants
        MeshRenderer[] meshRenderers = refObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = true;
        }

        // Désactiver tous les Rigidbodies attachés à cet objet et à ses enfants
        Rigidbody[] rigidbodies = refObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false; // Rendre le Rigidbody kinematic
            rb.detectCollisions = true; // Désactiver les collisions
        }

   
        // Retour à l'état Idle
        GetComponent<StateMachineDestructible>().ChangeState(GetComponent<IdleDestructible>());
    }

    public override void Tick(GameObject refObject) { }

    public override void Exit(GameObject refObject) { }
}
