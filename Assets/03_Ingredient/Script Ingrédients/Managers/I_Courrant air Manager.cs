using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_CourrantairManager : MonoBehaviour
{
    public void CourrantAir()
    {
        //  Son qui s'activee lorsequ'on est proche d'un cournant d'air
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Courrant d'air");
    }
}
