using System;
using UnityEngine;

public class CheckpointEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem activationParticle;
    [SerializeField] private ParticleSystem checkpointParticle;
    [SerializeField] private ParticleSystem checkpointArea;
    
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
            checkpointArea.Play();
        }

        if (!checkpointParticle.isPlaying)
        {
            checkpointParticle.Play();
        }
    }
}
