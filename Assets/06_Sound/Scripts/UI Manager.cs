using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void UI_bouton()
    {
        // Son qui s'active lorsque le joueur saute.
            
    }

    public void UI_bouton1()
    {
        // Son qui s'active lorsque le joueur utilise son deuxieme saut. 
        RuntimeManager.PlayOneShot("event:/Menu/bouton2");
    }

    public void UI_bouton2()
    {
        // Son qui s'active lorsque le joueur utilise son deuxieme saut. 
        RuntimeManager.PlayOneShot("event:/Menu/bouton3");
    }

    public void ambiance_hub()
    {
        // Son qui s'active lorsque le joueur utilise son deuxieme saut. 
        RuntimeManager.PlayOneShot("event:/Musique/Musique Hub");
    }

    public void ambiance_menu()
    {
        // Son qui s'active lorsque le joueur utilise son deuxieme saut. 
        RuntimeManager.PlayOneShot("event:/Menu/Musique Menu");
    }
}


    