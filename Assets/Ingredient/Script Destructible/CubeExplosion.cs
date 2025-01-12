using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [Tooltip("Prefab des mini-cubes � instancier.")]
    public GameObject miniCubePrefab;

    [Tooltip("Nombre de mini-cubes g�n�r�s.")]
    public int numberOfMiniCubes = 10;

    [Tooltip("Force appliqu�e aux mini-cubes lors de l'explosion.")]
    public float explosionForce = 5f;

    [Tooltip("Rayon autour du cube o� les mini-cubes sont plac�s.")]
    public float explosionRadius = 0.5f;

    [Tooltip("Dur�e avant la destruction des mini-cubes.")]
    public float miniCubeLifetime = 2f;

    /// <summary>
    /// D�clenche l'explosion, g�n�re les mini-cubes.
    /// </summary>
    public void Explode()
    {
        // G�n�re les mini-cubes
        for (int i = 0; i < numberOfMiniCubes; i++)
        {
            GenerateMiniCube();
        }
    }

    /// <summary>
    /// G�n�re un mini-cube avec une force d'explosion.
    /// </summary>
    private void GenerateMiniCube()
    {
        // Position al�atoire autour du cube
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * explosionRadius;

        // Instancie le mini-cube
        GameObject miniCube = Instantiate(miniCubePrefab, randomPosition, Random.rotation);

        // Applique une force d'explosion
        if (miniCube.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            Vector3 explosionDirection = (miniCube.transform.position - transform.position).normalized;
            rb.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
        }

        // D�truit le mini-cube apr�s un certain temps
        Destroy(miniCube, miniCubeLifetime);
    }
}
