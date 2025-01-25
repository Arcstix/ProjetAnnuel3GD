using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

 //INGREDIENTS 

    public void blocCassablePortal()
    {
        //  Son lorsequ'on casse l'igrédient portail
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Bloc cassable (portal)");
    }

    public void blocCassableViolet()
    {
        //  Son qui s'activee orsequ'on casse un bloc violet
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Bloc cassable (violet)");
    }
    public void interrupteur()
    {
        //  Son qui s'activee lorseque l'interrupteur s'active
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Interrupteur");
    }

    public void Collision()
    {
        //  !!! JE DEVRAI SUREMENT LE FAIRE MOI MEME CELUI CI !!! Son qui s'active lorsequ'on fait entrer en collision un objet déplacable. 
    }
    public void porte()
    {
        //  Son qui s'activee lorse que la porte s'ouvre
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Porte");
    }

}
