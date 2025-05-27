using UnityEngine;

public class GrowAnimator : MonoBehaviour
{
    private Material _material;
    [SerializeField] private string growProperty = "_Grow"; // Nom de la propriété dans le shader
    [SerializeField] private float growDuration = 5f;        // Durée pour aller de 0 à 1

    private float _timer;
    private bool _growing = true;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("GrowAnimator: Aucun Renderer trouvé sur ce GameObject.");
            enabled = false;
            return;
        }

        // Utiliser une instance du material pour éviter de modifier le matériau global
        _material = renderer.material;
    }

    private void Update()
    {
        if (_material == null) return;

        // Avancer ou reculer dans le temps selon la direction
        _timer += (_growing ? 1 : -1) * Time.deltaTime;

        // Calcul de la valeur normalisée entre 0 et 1
        float growValue = Mathf.Clamp01(_timer / growDuration);
        _material.SetFloat(growProperty, growValue);

        // Inversion du sens une fois les limites atteintes
        if (growValue >= 1f)
        {
            _growing = false;
        }
        else if (growValue <= 0f)
        {
            _growing = true;
        }
    }
}
