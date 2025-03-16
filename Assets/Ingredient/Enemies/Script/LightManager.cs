using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light spotLightWhite;
    public Light spotLightRed;
    //light search ?

    void Start()
    {
        EnableSpotLightWhite();
    }
        public void EnableSpotLightWhite()
        {
            spotLightRed.gameObject.SetActive(false);
            spotLightWhite.gameObject.SetActive(true);
        }
        public void EnableSpotLightRed()
        {
            spotLightWhite.gameObject.SetActive(false);
            spotLightRed.gameObject.SetActive(true);
        }
}
