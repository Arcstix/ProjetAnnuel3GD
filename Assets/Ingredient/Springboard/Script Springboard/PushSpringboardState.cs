using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PushSpringboardState : SpringboardState
{
    [SerializeField] private AnimationCurve pushCurve; // Courbe d'animation pour la projection
    [SerializeField] private float pushDuration; // Durée du saut
    private float pushTimer = 0f;
    public bool isPushing = false;
    public float pushForceMultiplier;
    public override void Enter(GameObject gameObject)
    {
        Debug.Log("PushSpringboardState");
    }

    public override void Tick(GameObject gameObject)
    {
        Debug.Log("Début du saut");

        if (!isPushing)
        {
            isPushing = true;
            StartCoroutine(ApplyPushForce());
        }
    }

    public override void Exit(GameObject gameObject)
    {
        float pushTimer = 0f;
    }
    private IEnumerator ApplyPushForce()
    {
       
        Rigidbody rb = player.GetComponentInChildren<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Aucun Rigidbody trouvé sur player !");
            yield break; // Stop la coroutine si pas de Rigidbody
        }

        while (pushTimer < pushDuration)
        {
            Debug.Log("Pendant le saut");
            pushTimer += Time.deltaTime;
            float t = pushTimer / pushDuration; // Normalisation du temps (0 à 1)
            float force = pushCurve.Evaluate(t) * pushForceMultiplier;
            rb.velocity += Vector3.up * force; // Applique la force en vertical

            yield return null; // Attend le prochain frame
        }

        Debug.Log("Fin du saut");

        isPushing = false;
        GetComponent<StateMachineSpringboard>().ChangeState(GetComponent<IdleSpringboardState>());
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GetComponent<StateMachineSpringboard>().ChangeState(GetComponent<IdleSpringboardState>());
            player = null;
        }
    }
}
