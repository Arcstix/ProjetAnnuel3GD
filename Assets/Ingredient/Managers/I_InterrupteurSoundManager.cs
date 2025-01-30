using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_InterrupteurSoundManager : MonoBehaviour
{
    public void Switch()
    {
        //  Son qui s'activee lorsque l'interrupteur s'active
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Interrupteur");
    }
}
