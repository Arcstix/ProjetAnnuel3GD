using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BallGaucheSoundManager : MonoBehaviour
{
    // Son qui s'active lorsque le projectile gauche n'est pas sur nous 
    public EventReference selectIdleSound;

    public KeyCode pressToplayIdleSound;
    
    FMOD.Studio.EventInstance soundIdleEvent;
    
    
    public EventReference selectActivationSound;

    public KeyCode pressToplayActivationSound;
    
    FMOD.Studio.EventInstance soundActivationEvent;

    void Start ()
    {
        soundIdleEvent = FMODUnity.RuntimeManager.CreateInstance(selectIdleSound);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundIdleEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
        soundActivationEvent = FMODUnity.RuntimeManager.CreateInstance(selectActivationSound);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundActivationEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    void Update()
    {
        // Son qui se joue quand on le place dans le vide
        PlayIdleSound();
        
        PlayActivationSound();
    }
    
    private void PlayIdleSound()
    { 
        if (Input.GetKeyDown(pressToplayIdleSound)) // la condition ici ce serait if la balle est pas sur le joueur (peut etre utiliser un bool)
        {
            FMOD.Studio.PLAYBACK_STATE fmodPbState;
            soundIdleEvent.getPlaybackState(out fmodPbState);
            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                soundIdleEvent.start();
            }
        }
        if (Input.GetKeyUp(pressToplayIdleSound)) // Ici la condition est si la balle revient sur le joueur
        {
            soundIdleEvent.stop (FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
    
    private void PlayActivationSound()
    {
        if (Input.GetKeyDown(pressToplayActivationSound)) // la condition ici ce serait if joueur se transporte vers le projectile 
        {
            FMOD.Studio.PLAYBACK_STATE fmodPbState;
            soundActivationEvent.getPlaybackState(out fmodPbState);
            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                soundActivationEvent.start();
            }
        }
        if (Input.GetKeyUp(pressToplayActivationSound)) // Ici la condiiton est si la balle reviens sur le joueur
        {
            soundActivationEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
    
    // public void ProjectileCollision()
        // {
        //     // !!!! A VOIR !!!!! Son qui s'active lorsque le projectile touche un objet/ingr�dient/mur/sol... (quelque chose). 
        //     FMODUnity.RuntimeManager.PlayOneShot("event:/Player/projectile collision");
        // }
}
