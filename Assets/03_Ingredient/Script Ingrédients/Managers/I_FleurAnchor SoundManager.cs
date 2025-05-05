using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_FleurAnchorSoundManager : MonoBehaviour
{
    public void FleurSaisie()
    {
        //  Son qui s'activee lorsequ'on a saisi une fleur(bourgeon) avec notre outil
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Fleur saisie");
    }

    public void FleurLancee()
    {
        //  Son qui s'activee lorsequ'on lance le bourgeon
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Fleur lancée");
    }

    public void FleurOuverture()
    {
        //  Son qui s'activee lorseque le bourgeon s'ouvre et deviens un acnhor
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Fleur ouverture");
    }
}
