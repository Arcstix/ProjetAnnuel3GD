using UnityEngine;

public class RespawnDestructible : DestructibleState
{
    public override void Enter(GameObject gameObject)
    {
        Debug.Log("Cube respawn");

        // Réactivation du mesh
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = true;
        }

        // Réactivation du collider
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        // Réinitialisation du Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = false;
        }

        // Retour à l'état Idle
        GetComponent<StateMachineDestructible>().ChangeState(GetComponent<IdleDestructible>());
    }

    public override void Tick(GameObject gameObject) { }

    public override void Exit(GameObject gameObject) { }
}
