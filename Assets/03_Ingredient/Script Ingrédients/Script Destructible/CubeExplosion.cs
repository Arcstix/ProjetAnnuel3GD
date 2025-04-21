using System;
using Unity.VisualScripting;
using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    [Tooltip("Particule qui apparaît lors de la destruction")]
    public ParticleSystem briseVitre;
    
    private IdleDestructible idleDestructible;
    
    private void Awake()
    {
        idleDestructible = GetComponent<IdleDestructible>();
    }

    private void OnEnable()
    {
        idleDestructible.DestructionEvent += Explode;
    }


    /// <summary>
    /// D�clenche l'explosion, g�n�re les mini-cubes.
    /// </summary>
    public void Explode(Vector3 direction)
    {
        Instantiate(briseVitre, transform.position, Quaternion.LookRotation(direction));
    }
}
