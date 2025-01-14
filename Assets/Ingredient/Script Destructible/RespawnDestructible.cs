using UnityEngine;

public class RespawnDestructible : DestructibleState
{
    public override void Enter(GameObject gameObject)
    {
        Debug.Log("Cube respawn");
        
        // Désactiver tous les Colliders attachés à cet objet et à ses enfants
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
        
        // Désactiver tous les MeshRenderers attachés à cet objet et à ses enfants
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = true;
        }

        // Désactiver tous les Rigidbodies attachés à cet objet et à ses enfants
        Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false; // Rendre le Rigidbody kinematic
            rigidbody.detectCollisions = true; // Désactiver les collisions
        }

   
        // Retour à l'état Idle
        GetComponent<StateMachineDestructible>().ChangeState(GetComponent<IdleDestructible>());
    }

    public override void Tick(GameObject gameObject) { }

    public override void Exit(GameObject gameObject) { }
}
