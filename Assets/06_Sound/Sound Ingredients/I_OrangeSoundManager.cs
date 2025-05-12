using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_OrangeSoundManager : MonoBehaviour
{
    public void CollisionOrangeIngredient()
    {
        // Son qui s'active lorsqu'on fait entrer en collision un objet d�placable 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/player collision");
    }
}
