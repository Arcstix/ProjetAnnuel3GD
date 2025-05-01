using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DissolveEntity : MonoBehaviour
{
    private static readonly int DissolveAmount = Shader.PropertyToID("_DissolveAmount");
    public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    public float dissolveSpeed = 1f;
    
    private List<Material> dissolveMats = new List<Material>();
    private float dissolveMaxAmount = 1.0f;
    private float dissolveMinAmount = 0f;
    private float currentDissolveAmount;
    private bool isDissolving = false;
    private bool isDissolved = false;
    
    private void Start()
    {
        if (meshRenderers.Count > 0)
        {
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                dissolveMats.Add(meshRenderer.material);
            }
            
            foreach (Material dissolveMat in dissolveMats)
            {
                dissolveMat.SetFloat(DissolveAmount, dissolveMinAmount);
            }
        }
    }

    private void Update()
    {
        // SEULEMENT POUR LES TESTS
        // if (Input.GetKeyDown(KeyCode.L) && !isDissolving && !isDissolved)
        // {
        //     isDissolving = true;
        //     currentDissolveAmount = dissolveMinAmount;
        //     PlayParticles();
        // }

        if (isDissolving && currentDissolveAmount < dissolveMaxAmount)
        {
            Dissolve();
        }
        else if(isDissolving && currentDissolveAmount > dissolveMinAmount)
        {
            isDissolving = false;
            isDissolved = true;
            currentDissolveAmount = dissolveMaxAmount;
        }
    }

    public void StartDissolve()
    {
        if (!isDissolving && !isDissolved)
        {
            isDissolving = true;
            currentDissolveAmount = dissolveMinAmount;
            PlayParticles();
        }
    }

    private void PlayParticles()
    {
        foreach (var particle in particleSystems)
        {
            particle.Play();
        }
    }

    private void Dissolve()
    {
        currentDissolveAmount += dissolveSpeed * Time.deltaTime;
        foreach (Material dissolveMat in dissolveMats)
        {
            dissolveMat.SetFloat(DissolveAmount, currentDissolveAmount);
        }
    }
}
