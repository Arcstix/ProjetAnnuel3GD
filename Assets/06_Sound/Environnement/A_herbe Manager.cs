using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_herbeManager : MonoBehaviour
{
    public void HerbeCollision()
    {
        //Son qui se dEclenche lorsqu'on touche une herbe
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/contact herbe");
    }
}
