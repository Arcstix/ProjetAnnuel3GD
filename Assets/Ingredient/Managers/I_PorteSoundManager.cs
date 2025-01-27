using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPorteSoundManager : MonoBehaviour
{
    public void porte()
    {
        //  Son qui s'activee lorse que la porte s'ouvre
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ingredients/Porte");
    }
}
