using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AntenneInteraction))]
public class AntenneVFX : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> particles = new List<ParticleSystem>();
    
    private AntenneInteraction antenneInteraction;

    private void Awake()
    {
        antenneInteraction = GetComponent<AntenneInteraction>();
    }

    private void Start()
    {
        antenneInteraction.OnToolInteraction += ToolInteraction;
    }

    private void ToolInteraction(bool isActive)
    {
        if (isActive)
        {
            foreach (ParticleSystem particle in particles)
            {
                particle.Play();
            }
        }
        else
        {
            foreach (ParticleSystem particle in particles)
            {
                particle.Stop();
            }
        }
    }
}
