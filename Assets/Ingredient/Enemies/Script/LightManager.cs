using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LightManager : MonoBehaviour
{
    [FormerlySerializedAs("spotLightWhite")] [Header("Lights")]
    public Light lightBase;
    [FormerlySerializedAs("spotLightRed")] 
    public Light lightDetected;

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
            lightDetected.gameObject.SetActive(false);
            lightBase.gameObject.SetActive(true);
        }
        public void EnableSpotLightRed()
        {
            lightBase.gameObject.SetActive(false);
            lightDetected.gameObject.SetActive(true);
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
            if (lightBase != null) lightBase.intensity = newIntensity;
            if (lightDetected != null) lightDetected.intensity = newIntensity;
        }
        #endregion
}
