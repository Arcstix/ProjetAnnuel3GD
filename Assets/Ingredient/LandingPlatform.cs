using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LandingPlatform : MonoBehaviour
{
    public bool isLanding = false;
    public Vector3 landingTarget = new Vector3(0, 0.5f, 0);
    
    private GameObject player;
    private bool onCoroutine = false;
    
    public void SetLandingState(bool state)
    {
        isLanding = state;
    }

    public bool IsLanding()
    {
        return isLanding;
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isLanding)
        {
            if (other.gameObject.GetComponent<PlayerMovementManager>().ReusableData.HadJump)
            {
                if (Vector3.Distance(other.gameObject.transform.position, transform.position + landingTarget) > 0.5f)
                {
                    other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    other.gameObject.transform.position =
                        Vector3.Lerp(other.gameObject.transform.position, transform.position + landingTarget, 0.2f);
                }
                else
                {
                    isLanding = false;
                    player = other.gameObject;
                }
                
                other.gameObject.GetComponent<PlayerMovementManager>().ReusableData.OnLandingPlatform = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && player != null)
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
        SetLandingState(true);
        onCoroutine = false;
    }
}
