using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [Header("Lights")]
    public Light spotLightWhite;
    public Light spotLightRed;

    [Header("Intensity Settings")]
    [Tooltip("Minimum d'intensité lumière")]
    public float minIntensity = 0f;
    [Tooltip("Maximum d'intensité lumière")]
    public float maxIntensity = 10f;
    [Tooltip("Vitesse de l'effet")]
    public float flickerSpeed = 1f; // Vitesse de l'effet

    [Header("Courbe d'intensité du changement de la lumière")]
    public AnimationCurve intensityCurve;

    private float elapsedTime = 0f;

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
        
        #region LightChanges
        // la tête de l'ennemi tourne de gauche à droite
        public void FlashLight()
        {
            Debug.Log("FlashLight");
            elapsedTime += Time.deltaTime * flickerSpeed;
        
            // Utilise la courbe pour interpoler l'intensité entre min et max
            float curveValue = intensityCurve.Evaluate(elapsedTime % 1f); 
            float newIntensity = Mathf.Lerp(minIntensity, maxIntensity, curveValue);

            // Applique la nouvelle intensité aux lumières
            if (spotLightWhite != null) spotLightWhite.intensity = newIntensity;
            if (spotLightRed != null) spotLightRed.intensity = newIntensity;
        }
        #endregion
}
