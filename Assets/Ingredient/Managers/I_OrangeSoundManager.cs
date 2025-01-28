using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_OrangeSoundManager : MonoBehaviour
{
    public void CollisionOrangeIngredient()
    {
        // Son qui s'active lorsequ'on fait entrer en collision un objet déplacable 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/player collision");
    }
}
