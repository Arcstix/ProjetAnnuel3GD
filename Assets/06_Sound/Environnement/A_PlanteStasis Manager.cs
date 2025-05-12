using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_PlanteStasisManager : MonoBehaviour
{
    public void SonPlanteStasis()
    {
        //Son qui se dEclenche lorsqu'on est proche d'une plante magique
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/Plantes ( énergie magique )");
    }
}

