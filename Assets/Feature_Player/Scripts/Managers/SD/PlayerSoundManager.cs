using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public void tirDroit()
    {
        //Son qui se déclenche lorsequ'on tire la balle droite
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/TIr droit");
    }

    public void tirGauche()
    {
        //Son qui se déclenche lorsequ'on tire la balle gauche
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Tir gauche");
    }
    public void rappelDroit()
    {
        //Son qui se déclenche lorsequ'on rappelle la balle droite
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/rappel droit");

    }
    public void rappelGauche()
    {
        //Son qui se déclenche lorsequ'on rappelle la balle gauche
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/rappel gauche");
    }

    public void activationDroit()
    {
        //Son qui se déclenche lorsequ'on active la balle droite
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/activation droit");
    }
    public void activationGauche()
    {
        //Son qui se déclenche lorsequ'on active la balle gauche
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/activation gauche");
    }
    public void gravityFreeze()
    {
        //  Son lors qu"on rentre en etat gravity Freeze
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/gravity freeze");
    }
    public void dash()
    {
        // Son orse qu'on active un outil pour dash vers lui
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/dash");
    }
    public void chute()
    {
        //  Son qui s'activee à la fin du gravity freeze
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Chute");
    }

    public void collision()
    {
        //  !!! JE DEVRAI SUREMENT LE FAIRE MOI MEME CELUI CI !!!  Son qui s'activee qaund le joueur entre en collision avec quelque chose
    }


}
