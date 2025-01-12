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

        // Désactivation du mesh
        GetComponent<MeshRenderer>().enabled = false;

        // Appelle la méthode Explode du script CubeExplosion attaché
        CubeExplosion explosionScript = GetComponent<CubeExplosion>();
        if (explosionScript != null)
        {
            explosionScript.Explode();
        }
        else
        {
            Debug.LogWarning("CubeExplosion script is missing on this GameObject!");
        }
    }

    public override void Tick(GameObject gameObject)
    {
        // Timer avant la réactivation
        timer += Time.deltaTime;
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
