using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPortalSoundManager : MonoBehaviour
{
    public void blocCassablePortal()
    {
        //  Son lorsequ'on casse l'igrédient portail
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Bloc cassable (portal)");
    }


}
