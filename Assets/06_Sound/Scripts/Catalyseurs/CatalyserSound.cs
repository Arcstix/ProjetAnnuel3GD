using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Serialization;

public class CatalyserSound : MonoBehaviour
{
    [Header("FMOD Sound")]
    // Son qui s'active lorsque le projectile gauche n'est pas sur nous 
    public EventReference idleSound;
    public EventReference activationSound;
    
    [Header("Debug Inputs")]
    public KeyCode pressToplayIdleSound;
    public KeyCode pressToplayActivationSound;
    
    FMOD.Studio.EventInstance soundIdleEvent;
    FMOD.Studio.EventInstance soundActivationEvent;
    
    private ToolInteraction toolInteraction;

    private void Awake()
    {
        toolInteraction = GetComponent<ToolInteraction>();
    }

    void Start ()
    {
        soundIdleEvent = RuntimeManager.CreateInstance(idleSound);
        RuntimeManager.AttachInstanceToGameObject(soundIdleEvent, GetComponent<Transform>());
        soundActivationEvent = RuntimeManager.CreateInstance(activationSound);
        RuntimeManager.AttachInstanceToGameObject(soundActivationEvent, GetComponent<Transform>());

        if (toolInteraction)
        {
            toolInteraction.OnInteractionStart += PlayActivationSound;
            toolInteraction.OnInteractionEnd += PlayIdleSound;
        }
    }

    private void Update()
    {
        soundIdleEvent.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        soundActivationEvent.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
    }

    public void PlayIdleSound()
    { 
        StopActivationSound();
        soundIdleEvent.getPlaybackState(out var fmodPbState);
        if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            soundIdleEvent.start();
        }
        
        if (Input.GetKeyDown(pressToplayIdleSound)) // Debug in case the sound doesn't play
        {
            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                soundIdleEvent.start();
            }
        }
    }

    private void StopIdleSound()
    {
        soundIdleEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        
        if (Input.GetKeyUp(pressToplayIdleSound)) // Debug in case the sound doesn't play
        {
            soundIdleEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
    
    private void PlayActivationSound()
    {
        StopIdleSound();
        soundActivationEvent.getPlaybackState(out var fmodPbState);
        if (Input.GetKeyDown(pressToplayActivationSound)) // Debug in case the sound doesn't play 
        {
            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                soundActivationEvent.start();
            }
        }
        
        if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            soundActivationEvent.start();
        }
    }

    private void StopActivationSound()
    {
        if (Input.GetKeyUp(pressToplayActivationSound)) // Debug in case the sound doesn't play
        {
            soundActivationEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        
        soundActivationEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StopAllSound()
    {
        StopIdleSound();
        StopActivationSound();
    }

    private void OnDestroy()
    {
        StopAllSound();
    }
}
