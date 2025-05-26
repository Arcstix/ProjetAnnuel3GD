using System;
using System.Net.Mail;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class I_EnnemisSoundManager : MonoBehaviour
{
    public KeyCode pressToPlayIdleSound;
    public KeyCode pressToPlayAttackSound;
    public KeyCode pressToPlayDeathSound;
    public KeyCode pressToPlayDetectionSound;
    
    
    public EventReference selectIdleSound;
    public EventReference selectAttackSound;
    public EventReference selectDeathSound;
    public EventReference selectDetectionSound;

    
    private EventInstance soundIdleEvent;
    private EventInstance soundAttackEvent;
    private EventInstance soundDeathEvent;
    private EventInstance soundDetectionEvent;
    
    private IdleEnemyState idleState;
    private AlertEnemyState alertState;
    private EnemyDeadState deadState;

    private void Awake()
    {
        idleState = GetComponent<IdleEnemyState>();
        alertState = GetComponent<AlertEnemyState>();
        deadState = GetComponent<EnemyDeadState>();
    }

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

        idleState.OnEnterIdle += EnterIdleSound;
        idleState.OnExitIdle += ExitIdleSound;
        alertState.OnEnterAlert += EnterDetectionSound;
        alertState.OnEnterAlert += EnterAttackSound;
        alertState.OnExitAlert += ExitDetectionSound;
        alertState.OnExitAlert += ExitAttackSound;
        deadState.OnEnterDeath += PlayDeathSound;
        
        EnterIdleSound();
    }

    private void OnDisable()
    {
        idleState.OnEnterIdle -= EnterIdleSound;
        idleState.OnExitIdle -= ExitIdleSound;
        alertState.OnEnterAlert -= EnterDetectionSound;
        alertState.OnEnterAlert -= EnterAttackSound;
        alertState.OnExitAlert -= ExitDetectionSound;
        alertState.OnExitAlert -= ExitAttackSound;
        deadState.OnEnterDeath -= PlayDeathSound;
    }

    void Update()
    {
        if (Input.GetKeyDown(pressToPlayIdleSound)) // la condition ici ce serait quand l'ennemi est en etat IDLE
        {
            soundIdleEvent.getPlaybackState(out var fmodPbState);
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
        
        if (Input.GetKeyDown(pressToPlayDetectionSound)) // La condition ici serait quand l'ennemi detecte le joueur 
        {
            soundIdleEvent.getPlaybackState(out var fmodPbState);
            if (fmodPbState != PLAYBACK_STATE.PLAYING)
            {
                soundDetectionEvent.start();
            }
        }
        
        if (Input.GetKeyUp(pressToPlayDetectionSound))
        {
            soundDetectionEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        
        if (Input.GetKeyDown(pressToPlayAttackSound)) // La condition ici serait quand l'ennemi est en etat "attaque" (nous brule) 
        {
            soundIdleEvent.getPlaybackState(out var fmodPbState);
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
        
        if (Input.GetKeyDown(pressToPlayDeathSound)) // La condition ici serait quand l'ennemi meurt
        {
            soundIdleEvent.getPlaybackState(out var fmodPbState);
            if (fmodPbState != PLAYBACK_STATE.PLAYING)
            {
                soundDeathEvent.start();
            }
        }
    }
    
    void EnterIdleSound()
    {
        soundIdleEvent.getPlaybackState(out var fmodPbState);
        
        if (fmodPbState != PLAYBACK_STATE.PLAYING)
        {
            soundIdleEvent.setParameterByName("Etat Idle", 0f);
            soundIdleEvent.start();
        }
    }

    void ExitIdleSound()
    {
        soundIdleEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        soundIdleEvent.setParameterByName("Etat Idle", 1f);
    }
    
    void EnterDetectionSound()
    {
        soundDetectionEvent.getPlaybackState(out var fmodPbState);
        
        if (fmodPbState != PLAYBACK_STATE.PLAYING)
        {
            soundDetectionEvent.start();
        }
    }

    void ExitDetectionSound()
    {
        soundDetectionEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    
    void EnterAttackSound()
    {
        soundAttackEvent.getPlaybackState(out var fmodPbState);
        
        if (fmodPbState != PLAYBACK_STATE.PLAYING)
        {
            soundAttackEvent.setParameterByName("Etat attaque", 0f);
            soundAttackEvent.start();
        }
    }

    void ExitAttackSound()
    {
        soundAttackEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        soundAttackEvent.setParameterByName("Etat attaque", 1f);
    }
    
    void PlayDeathSound()
    {
        soundDeathEvent.getPlaybackState(out var fmodPbState);
        
        if (fmodPbState != PLAYBACK_STATE.PLAYING)
        {
            soundDeathEvent.start();
        }
    }
}
