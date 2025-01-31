using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_PortalSoundManager : MonoBehaviour
{
    private DestroyDestructible destroyDestructible;

    private void Awake()
    {
        destroyDestructible = GetComponent<DestroyDestructible>();
    }

    private void OnEnable()
    {
        destroyDestructible.OnDestroyed += PortalDestroySound;
    }

    private void PortalDestroySound()
    {
        //  Son lorsqu'on casse l'igr�dient portail
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Bloc cassable (portal)", transform.position);
    }
}
