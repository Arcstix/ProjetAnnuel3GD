using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [Tooltip("Prefab des mini-cubes à instancier.")]
    public GameObject miniCubePrefab;

    [Tooltip("Nombre de mini-cubes générés.")]
    public int numberOfMiniCubes = 10;

    [Tooltip("Force appliquée aux mini-cubes lors de l'explosion.")]
    public float explosionForce = 5f;

    [Tooltip("Rayon autour du cube où les mini-cubes sont placés.")]
    public float explosionRadius = 0.5f;

    [Tooltip("Durée avant la destruction des mini-cubes.")]
    public float miniCubeLifetime = 2f;

    /// <summary>
    /// Déclenche l'explosion, génère les mini-cubes.
    /// </summary>
    public void Explode()
    {
        // Génère les mini-cubes
        for (int i = 0; i < numberOfMiniCubes; i++)
        {
            GenerateMiniCube();
        }
    }

    /// <summary>
    /// Génère un mini-cube avec une force d'explosion.
    /// </summary>
    private void GenerateMiniCube()
    {
        // Position aléatoire autour du cube
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * explosionRadius;

        // Instancie le mini-cube
        GameObject miniCube = Instantiate(miniCubePrefab, randomPosition, Random.rotation);

        // Applique une force d'explosion
        if (miniCube.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            Vector3 explosionDirection = (miniCube.transform.position - transform.position).normalized;
            rb.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
        }

        // Détruit le mini-cube après un certain temps
        Destroy(miniCube, miniCubeLifetime);
    }
}
