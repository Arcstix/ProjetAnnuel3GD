using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_PortalSoundManager : MonoBehaviour
{
    public void blocCassablePortal()
    {
        //  Son lorsequ'on casse l'igr�dient portail
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Bloc cassable (portal)");
    }


}
