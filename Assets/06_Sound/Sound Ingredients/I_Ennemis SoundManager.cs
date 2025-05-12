using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class I_EnnemisSoundManager : MonoBehaviour
{
    public EventReference selectIdleSound;

    public KeyCode pressToPlayIdleSound;
    
    private EventInstance soundIdleEvent;
    
    public EventReference selectAttackSound;

    public KeyCode pressToPlayAttackSound;
    
    private EventInstance soundAttackEvent;
    
    public EventReference selectDeathSound;

    public KeyCode pressToPlayDeathSound;
    
    private EventInstance soundDeathEvent;
    
    public EventReference selectDetectionSound;

    public KeyCode pressToPlayDetectionSound;
    
    private EventInstance soundDetectionEvent;

    void Start()
    {
        soundIdleEvent = RuntimeManager.CreateInstance(selectIdleSound);
        RuntimeManager.AttachInstanceToGameObject(soundIdleEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
        soundDetectionEvent = RuntimeManager.CreateInstance(selectDetectionSound);
        RuntimeManager.AttachInstanceToGameObject(soundDetectionEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
        soundAttackEvent = RuntimeManager.CreateInstance(selectAttackSound);
        RuntimeManager.AttachInstanceToGameObject(soundAttackEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
        soundDeathEvent = RuntimeManager.CreateInstance(selectDeathSound);
        RuntimeManager.AttachInstanceToGameObject(soundDeathEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    void Update()
    {
        PlayIdleSound();
        PlayDetectionSound();
        PlayAttackSound();
        PlayDeathSound();
    }
    
    void PlayIdleSound()
    {
        if (Input.GetKeyDown(pressToPlayIdleSound)) // la condition ici ce serait quand l'ennemi est en etat IDLE
        {
            PLAYBACK_STATE fmodPbState;
            soundIdleEvent.getPlaybackState(out fmodPbState);
            if (fmodPbState != PLAYBACK_STATE.PLAYING)
            {
                soundIdleEvent.setParameterByName("Etat Idle", 0f);
                soundIdleEvent.start();
            }
        }
        if (Input.GetKeyUp(pressToPlayIdleSound)) // Ici la condiiton est s'il sort de l'etat idle
        {
            soundIdleEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            soundIdleEvent.setParameterByName("Etat Idle", 1f);
        }
    }
    
    void PlayDetectionSound()
    {
        if (Input.GetKeyDown(pressToPlayDetectionSound)) // La condition ici serait quand l'ennemi detecte le joueur 
        {
            PLAYBACK_STATE fmodPbState;
            soundDetectionEvent.getPlaybackState(out fmodPbState);
            if (fmodPbState != PLAYBACK_STATE.PLAYING)
            {
                soundDetectionEvent.start();
            }
        }
        if (Input.GetKeyUp(pressToPlayDetectionSound)) // 
        {
            soundDetectionEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
    
    void PlayAttackSound()
    {
        if (Input.GetKeyDown(pressToPlayAttackSound)) // La condition ici serait quand l'ennemi est en etat "attaque" (nous brule) 
        {
            PLAYBACK_STATE fmodPbState;
            soundAttackEvent.getPlaybackState(out fmodPbState);
            if (fmodPbState != PLAYBACK_STATE.PLAYING)
            {
                soundAttackEvent.setParameterByName("Etat attaque", 0f);
                soundAttackEvent.start();
            }
        }
        if (Input.GetKeyUp(pressToPlayAttackSound)) // 
        {
            soundAttackEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            soundAttackEvent.setParameterByName("Etat attaque", 1f);
        }
    }
    
    void PlayDeathSound()
    {
        if (Input.GetKeyDown(pressToPlayDeathSound)) // La condition ici serait quand l'ennemi meurt
        {
            PLAYBACK_STATE fmodPbState;
            soundDeathEvent.getPlaybackState(out fmodPbState);
            if (fmodPbState != PLAYBACK_STATE.PLAYING)
            {
                soundDeathEvent.start();
            }
        }
        if (Input.GetKeyUp(pressToPlayDeathSound)) // 
        {
            soundDeathEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
