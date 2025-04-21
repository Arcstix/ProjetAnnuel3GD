using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_InterrupteurSoundManager : MonoBehaviour
{
    private ActivationSwitch activationSwitch;

    private void Awake()
    {
        activationSwitch = GetComponent<ActivationSwitch>();
    }

    private void OnEnable()
    {
        activationSwitch.onActivate += Switch;
    }

    public void Switch()
    {
        //  Son qui s'activee lorsque l'interrupteur s'active
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Interrupteur", transform.position);
    }
}
