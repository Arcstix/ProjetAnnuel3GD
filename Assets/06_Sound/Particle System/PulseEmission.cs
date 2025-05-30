using UnityEngine;

public class PulseEmission : MonoBehaviour
{
    public Material mat;                     // Matériau à faire pulser
    public float pulseSpeed = 2f;            // Vitesse du battement
    public float baseIntensity = 1f;         // Intensité de base
    public float pulseAmount = 2f;           // Variation d'intensité

    void Update()
    {
        float intensity = baseIntensity + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        mat.SetColor("_EmissionColor", Color.cyan * intensity); // Tu peux changer Color.cyan par une autre couleur
    }
}