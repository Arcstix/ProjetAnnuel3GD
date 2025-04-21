using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_PorteSoundManager : MonoBehaviour
{
    private DoorManager doorManager;

    private void Awake()
    {
        doorManager = GetComponent<DoorManager>();
    }

    private void OnEnable()
    {
        doorManager.OnOpen += Door;
    }

    private void Door()
    {
        //  Son qui s'active lorsque la porte s'ouvre
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Porte", transform.position);
    }
}
