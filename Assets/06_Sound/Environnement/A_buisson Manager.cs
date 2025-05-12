using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_buissonManager : MonoBehaviour
{
    public void BuissonCollision()
    {
        //Son qui se dEclenche lorsqu'on touche un buisson
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/contact buisson");
    }
}
