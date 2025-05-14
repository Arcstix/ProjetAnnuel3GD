using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_VioletSoundManager : MonoBehaviour
{
    public void blocCassableViolet()
    {
        //  Son qui s'activee orsequ'on casse un bloc violet
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Bloc cassable (violet)");
    }
}
