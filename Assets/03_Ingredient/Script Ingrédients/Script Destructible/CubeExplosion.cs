using System;
using Unity.VisualScripting;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [Tooltip("Particule qui apparaît lors de la destruction")]
    public ParticleSystem briseVitre;
    
    private IdleDestructible idleDestructible;
    private BoosterDetection boosterDetection;
    
    private void Awake()
    {
        idleDestructible = GetComponent<IdleDestructible>();
        boosterDetection = GetComponent<BoosterDetection>();
    }

    private void OnEnable()
    {
        if (idleDestructible)
        {
            idleDestructible.DestructionEvent += Explode;
        }
    }

    public void EmitParticule(GameObject refPlayer)
    {
        Vector3 direction = refPlayer.GetComponent<Rigidbody>().velocity.normalized;
        
        Explode(direction);
    }

    /// <summary>
    /// D�clenche l'explosion, g�n�re les mini-cubes.
    /// </summary>
    public void Explode(Vector3 direction)
    {
        Instantiate(briseVitre, transform.position, Quaternion.LookRotation(direction));
    }
    
    
}
