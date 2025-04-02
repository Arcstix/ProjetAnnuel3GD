using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PushSpringboardState : SpringboardState
{
    [SerializeField] private AnimationCurve pushCurve; // Courbe d'animation pour la projection
    [SerializeField] private float pushDuration; // Durée du saut
    public float pushTimer = 0f;
    public bool isPushing = true;
    public float pushForceMultiplier;
    public override void Enter(GameObject gameObject)
    {
        Debug.Log("PushSpringboardState");
        isPushing = true;
    }

    public override void Tick(GameObject gameObject)
    {
        Debug.Log("Début du saut");

        if (isPushing && pushTimer == 0f) // Ne lancer la coroutine qu'une seule fois
        {
            StartCoroutine(ApplyPushForce());
        }
        
        if (!isPushing)
        {
            pushTimer = 0f;
            GetComponent<StateMachineSpringboard>().ChangeState(GetComponent<IdleSpringboardState>());
        }
    }

    public override void Exit(GameObject gameObject)
    {
        float pushTimer = 0f;
    }
    private IEnumerator ApplyPushForce()
    {
        Rigidbody rb = player.GetComponentInChildren<Rigidbody>();
        pushTimer = 0f; // Réinitialisation du timer

        while (pushTimer < pushDuration)
        {
            Debug.Log("Pendant le saut");
            pushTimer += Time.deltaTime;
            float t = pushTimer / pushDuration; // Normalisation du temps (0 à 1)
            float force = pushCurve.Evaluate(t) * pushForceMultiplier;
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);

            yield return null; // Attend le prochain frame
        }

        // Assurer que la valeur finale est bien atteinte
        pushTimer = pushDuration;
        isPushing = false;
        pushTimer = 0f;
        Debug.Log("Fin du saut");
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
