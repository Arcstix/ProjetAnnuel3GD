using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LandingPlatform : MonoBehaviour
{
    public bool isAvailable = true;
    public Vector3 landingTarget = new Vector3(0, 0.5f, 0);
    
    private GameObject player;
    private bool onCoroutine = false;
    
    public void SetAvailableState(bool state)
    {
        isAvailable = state;
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isAvailable)
        {
            if (other.gameObject.GetComponent<PlayerMovementManager>().ReusableData.HadJump)
            {
                MovementStateMachine stateMachine = other.gameObject.GetComponent<PlayerMovementManager>().StateMachine;
                PlayerReusableStateData reusableData = other.gameObject.GetComponent<PlayerMovementManager>().ReusableData;
                reusableData.OnLandingPlatform = true;
                reusableData.TargetPosition = transform.position + landingTarget;
                isAvailable = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!onCoroutine)
            {
                onCoroutine = true;
                StartCoroutine(ResetLandingState());
            }
            other.gameObject.GetComponent<PlayerMovementManager>().ReusableData.OnLandingPlatform = false;
        }
    }

    private IEnumerator ResetLandingState()
    {
        yield return new WaitForSeconds(1);
        SetAvailableState(true);
        onCoroutine = false;
    }
}
