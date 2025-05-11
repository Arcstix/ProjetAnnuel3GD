//using Palmmedia.ReportGenerator.Core.Reporting.Builders;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class I_FleurAnchorSoundManager : MonoBehaviour
//{
//    public void FleurSaisie()
//    {
//        //  Son qui s'activee lorsequ'on a saisi une fleur(bourgeon) avec notre outil
//        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Fleur saisie");

//        private void Start()
//        {
//            SonFleurSaisie = FMODUnity.RuntimeManager.CreateInstance("event:/Ingredients/Fleur saisie");
//        }

//        SonFleurSaisie.setParameterByName("FinSaisie", 0f); // lancer ca quauand on a saisi le bourgeon avec notre outil
//        SonFleurSaisie.start(); // lancer ca quauand on a saisi le bourgeon avec notre outil

//        SonFleurSaisie.setParameterByName("FinSaisie", 1f); // lancer ça au moment ou on balance la fleur pour arreter le son de saisie

//    }

//    public void FleurLancee()
//    {
//        //  Son qui s'activee lorsequ'on lance le bourgeon
//        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Fleur lancée");
//    }

//    public void FleurOuverture()
//    {
//        //  Son qui s'activee lorseque le bourgeon s'ouvre et deviens un acnhor (s'accroche a un mur gris en gros)
//        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Fleur ouverture");
//    }

//    public void FleurCollision()
//    {
//        //  Son qui s'activee lorseque le bourgeon collisionnes un autre objet (mur, sol, etc...)
//        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Objet collision");
//    }

//}
