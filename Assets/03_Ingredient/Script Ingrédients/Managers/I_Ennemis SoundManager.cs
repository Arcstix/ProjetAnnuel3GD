using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_EnnemisSoundManager : MonoBehaviour
{
    public void IdleEnnemi()
    {
        //  Son qui s'activee lorseque l'ennemi est en etat idle
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ennemies/Ennemies Idle");
    }

    public void EnnemiDeath()
    {
        //  Son qui s'activee lorseque l'ennemi meurt
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ennemies/Ennemies death");
    }

    public void EnneliDetection()
    {
        //  Son qui s'activee lorseque l'ennemi nous detecte
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ennemies/Ennemies détection");
    }

    public void EnneliAttaque()
    {
        //  Son qui s'activee lorseque l'ennemi nous brule
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ennemies/Ennemies charge attaque");
    }
}
