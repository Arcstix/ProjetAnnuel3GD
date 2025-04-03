using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PushSpringboardState : SpringboardState
{
    [SerializeField] private AnimationCurve pushCurve; // Courbe d'animation pour la projection
    [SerializeField] private float pushDuration; // Durée du saut
    public float pushTimer = 0f;
    public float pushForceMultiplier;
    public override void Enter(GameObject gameObject)
    {
        Debug.Log("PushSpringboardState");
        PlayerMovementManager movementManager = player.GetComponent<PlayerMovementManager>();
        movementManager.hasExternalForces = true;
    }

    public override void Tick(GameObject gameObject)
    {
        Debug.Log("Début du saut");

        if (pushTimer < pushDuration) // Ne lancer la coroutine qu'une seule fois
        {
            pushTimer += Time.deltaTime;
            ApplyPushForce();
        }
        else
        {
            GetComponent<StateMachineSpringboard>().ChangeState(GetComponent<IdleSpringboardState>());
        }
        
    }

    public override void Exit(GameObject gameObject)
    {
       pushTimer = 0f;
       PlayerMovementManager movementManager = player.GetComponent<PlayerMovementManager>();
       movementManager.hasExternalForces = false;
    }
    private void ApplyPushForce()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        
        Debug.Log("Pendant le saut"); 
       
        float t = pushTimer / pushDuration; // Normalisation du temps (0 à 1)
        float force = pushCurve.Evaluate(t) * pushForceMultiplier;
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
}
