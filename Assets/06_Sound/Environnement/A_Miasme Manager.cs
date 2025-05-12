using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public void SonMiasme()
    {
        //Son qui se dEclenche proche d'un miasme
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/Miasme");
    }
}

