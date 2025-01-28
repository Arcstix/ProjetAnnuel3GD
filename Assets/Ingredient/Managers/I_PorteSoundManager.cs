using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_PorteSoundManager : MonoBehaviour
{
    public void porte()
    {
        //  Son qui s'activee lorse que la porte s'ouvre
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Porte");
    }
}
