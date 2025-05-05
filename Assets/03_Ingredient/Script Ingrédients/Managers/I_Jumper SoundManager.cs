using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_JumperSoundManager : MonoBehaviour
{
    public void Bouncer()
    {
        //  Son qui s'activee lorsequ'on a touche un bouncer
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Bouncer");
    }
}
