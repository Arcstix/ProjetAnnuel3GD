using System;
using UnityEngine;

public class CheckpointEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem activationParticle;
    [SerializeField] private ParticleSystem checkpointParticle;
    
    private CheckpointDetection checkpointDetection;
    
    private bool isActivated = false;

    private void Awake()
    {
        checkpointDetection = GetComponent<CheckpointDetection>();
        isActivated = false;
        checkpointDetection.OnCheckpointActivated += PlayParticle;
    }

    private void PlayParticle()
    {
        if (!isActivated)
        {
            isActivated = true;
            activationParticle.Play();
        }

        if (!checkpointParticle.isPlaying)
        {
            checkpointParticle.Play();
        }
    }
}
