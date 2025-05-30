using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void UI_bouton()
    {
        // Son qui s'active lorsque le joueur saute.
        RuntimeManager.PlayOneShot("event:/Menu/bouton1");
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
}
