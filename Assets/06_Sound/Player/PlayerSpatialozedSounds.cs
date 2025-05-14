using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Serialization;

public class PlayerSpatializedSound : MonoBehaviour
{
    [Header("FMOD Sound")]
    // Son qui s'active lorsque le projectile gauche n'est pas sur nous 
    public EventReference tirSound;
    public EventReference rappelSound;

    [Header("Debug Inputs")]
    public KeyCode pressToplayTirSound;
    public KeyCode pressToplayRappelSound;

    FMOD.Studio.EventInstance soundTirEvent;
    FMOD.Studio.EventInstance soundRappelEvent;

    private ToolInteraction toolInteraction;



    void Start()
    {
        soundTirEvent = RuntimeManager.CreateInstance(tirSound);
        RuntimeManager.AttachInstanceToGameObject(soundTirEvent, GetComponent<Transform>());
        soundRappelEvent = RuntimeManager.CreateInstance(rappelSound);
        RuntimeManager.AttachInstanceToGameObject(soundRappelEvent, GetComponent<Transform>());
    }

    public void PlayTirSound()
    {
        StopTirSound();
        soundTirEvent.getPlaybackState(out var fmodPbState);
        if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            soundTirEvent.start();
        }

        if (Input.GetKeyDown(pressToplayTirSound)) // Debug in case the sound doesn't play
        {
            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                soundTirEvent.start();
            }
        }
    }

    private void StopTirSound()
    {
        soundTirEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        if (Input.GetKeyUp(pressToplayTirSound)) // Debug in case the sound doesn't play
        {
            soundTirEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void PlayRappelSound()
    {
        StopTirSound();
        soundRappelEvent.getPlaybackState(out var fmodPbState);
        if (Input.GetKeyDown(pressToplayRappelSound)) // Debug in case the sound doesn't play 
        {
            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                soundRappelEvent.start();
            }
        }

        if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            soundRappelEvent.start();
        }
    }

    private void StopRappelSound()
    {
        if (Input.GetKeyUp(pressToplayRappelSound)) // Debug in case the sound doesn't play
        {
            soundRappelEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        soundRappelEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StopAllSound()
    {
        StopTirSound();
        StopRappelSound();
    }

    private void OnDestroy()
    {
        StopAllSound();
    }
}
