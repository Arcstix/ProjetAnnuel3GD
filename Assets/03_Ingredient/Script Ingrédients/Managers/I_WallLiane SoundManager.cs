using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_WallLianeSoundManager : MonoBehaviour
{
    public void WallLiane()
    {
        //  Son qui s'activee lorsequ'on a lancé notre outil sur l'anchor d'un wallrun
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Lianes wall run");
    }
}
