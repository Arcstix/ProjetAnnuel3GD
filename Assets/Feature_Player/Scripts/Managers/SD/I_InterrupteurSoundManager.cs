using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInterrupteurSoundManager : MonoBehaviour
{
    public void interrupteur()
    {
        //  Son qui s'activee lorseque l'interrupteur s'active
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Interrupteur");
    }
}
