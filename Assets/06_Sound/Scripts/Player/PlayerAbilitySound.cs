using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class PlayerAbilitySound : MonoBehaviour
{
    public KeyCode pressToPlaySonAccordDashSound;
    [SerializeField] private int level1DashAccord = 2;
    [SerializeField] private int level2DashAccord = 3;
    [SerializeField] private int level3DashAccord = 4;
    
    private PlayerAbilityManager abilityManager;
    private EventInstance SonAccordDash;

    private int counter = 0;

    public EventReference tirSound;
    public EventReference rappelSound;
    public EventReference tirGaucheSound;

    [Header("Debug Inputs")]
    public KeyCode pressToplayTirSound;
    public KeyCode pressToplayRappelSound;
    public KeyCode pressToplayTirGaucheSound;


    FMOD.Studio.EventInstance soundTirEvent;
    FMOD.Studio.EventInstance soundTirGaucheEvent;
    FMOD.Studio.EventInstance soundRappelEvent;

    private void Awake()
    {
        abilityManager = GetComponent<PlayerAbilityManager>();
        abilityManager.OnAbilityStarted += InitAbility;
    }

    private void Start()
    {
        SonAccordDash = RuntimeManager.CreateInstance("event:/Musique/Accords dash");

        soundTirGaucheEvent = RuntimeManager.CreateInstance(tirGaucheSound);
        RuntimeManager.AttachInstanceToGameObject(soundTirGaucheEvent, GetComponent<Transform>());
        soundTirEvent = RuntimeManager.CreateInstance(tirSound);
        RuntimeManager.AttachInstanceToGameObject(soundTirEvent, GetComponent<Transform>());
        soundRappelEvent = RuntimeManager.CreateInstance(rappelSound);
        RuntimeManager.AttachInstanceToGameObject(soundRappelEvent, GetComponent<Transform>());
    }


    private void InitAbility()
    {
        abilityManager.AbilityStateMachine.ShootState.OnRightShoot += RightShoot;
        abilityManager.AbilityStateMachine.ShootState.OnLeftShoot += LeftShoot;
        abilityManager.AbilityStateMachine.RecallAllState.OnRightRecall += RightRecallAll;
        abilityManager.AbilityStateMachine.RecallAllState.OnLeftRecall += LeftRecallAll;
        abilityManager.AbilityStateMachine.MovePlayer.EnterDash += Dash;
        abilityManager.AbilityStateMachine.AimState.OnAirAim += EnterGravityFreeze;
        abilityManager.AbilityStateMachine.AimState.OnExitAim += QuitGravityFreeze;
        abilityManager.AbilityStateMachine.MovePlayer.OnConsecutiveDashes += ConsecutiveDashes;
        abilityManager.AbilityStateMachine.MovePlayer.OnFlyingDance += FlyingDance;
    }

    private void OnDisable()
    {
        abilityManager.AbilityStateMachine.ShootState.OnRightShoot -= RightShoot;
        abilityManager.AbilityStateMachine.ShootState.OnLeftShoot -= LeftShoot;
        abilityManager.AbilityStateMachine.RecallAllState.OnRightRecall -= RightRecallAll;
        abilityManager.AbilityStateMachine.RecallAllState.OnLeftRecall -= LeftRecallAll;
        abilityManager.AbilityStateMachine.MovePlayer.EnterDash -= Dash;
        abilityManager.AbilityStateMachine.MovePlayer.OnFlyingDance -= FlyingDance;
    }

    private void Update()
    {
        soundTirEvent.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        soundTirGaucheEvent.set3DAttributes(RuntimeUtils.To3DAttributes(transform));

        if (Input.GetKeyDown(pressToPlaySonAccordDashSound))
        {
            if (counter == 0)
            {
                Level1Dash();
            }

            if (counter == 1)
            {
                Level2Dash();
            }

            if (counter == 2)
            {
                Level3Dash();
            }
            counter++;
            if (counter == 3)
            {
                counter = 0;
            }
        }
    }

    private void RightShoot()
    {
        //Son qui se d�clenche lorsqu'on tire la balle droite
        //RuntimeManager.PlayOneShot("event:/Player/TIr droit");

        Debug.Log("Bonjour!");
      //  StopTirSound();
        soundTirEvent.getPlaybackState(out var fmodPbState);
        if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            Debug.Log("ALLOOO!");
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

    private void LeftShoot()
    {
        //Son qui se d�clenche lorsqu'on tire la balle gauche
        RuntimeManager.PlayOneShot("event:/Player/Tir gauche");

        Debug.Log("Bonjour!");
        //  StopTirSound();
        soundTirGaucheEvent.getPlaybackState(out var fmodPbState);
        if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            Debug.Log("ALLOOO!");
            soundTirGaucheEvent.start();
        }

        if (Input.GetKeyDown(pressToplayTirGaucheSound)) // Debug in case the sound doesn't play
        {
            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                soundTirGaucheEvent.start();
            }
        }
    }
    
    private void RightRecallAll()
    {
        //Son qui se d�clenche lorsqu'on rappelle la balle droite
        RuntimeManager.PlayOneShot("event:/Player/rappel droit");

        Debug.Log("coucou!");
        //  StopTirSound();
        soundRappelEvent.getPlaybackState(out var fmodPbState);
        if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            Debug.Log("oki!");
            soundRappelEvent.start();
        }

        if (Input.GetKeyDown(pressToplayTirSound)) // Debug in case the sound doesn't play
        {
            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                soundRappelEvent.start();
            }
        }
    }
    
    private void LeftRecallAll()
    {
        //Son qui se d�clenche lorsqu'on rappelle la balle gauche
        RuntimeManager.PlayOneShot("event:/Player/rappel gauche");
    }
    
    private void EnterGravityFreeze()
    {
        //  Son lorsqu"on rentre en etat gravity Freeze
        RuntimeManager.PlayOneShot("event:/Player/gravity freeze");
    }
    
    private void QuitGravityFreeze()
    {
        //  Son qui s'active � la fin du gravity freeze (Chute)
        RuntimeManager.PlayOneShot("event:/Player/Chute");
    }
    
    private void Dash()
    {
        // Son lorsqu'on active un outil pour dash vers lui
        RuntimeManager.PlayOneShot("event:/Player/dash");
    }
    
    private void ConsecutiveDashes(int numberOfDashes)
    {
        if (numberOfDashes >= level1DashAccord && numberOfDashes < level2DashAccord)
        {
            Level1Dash();
        }

        if (numberOfDashes >= level2DashAccord && numberOfDashes < level3DashAccord)
        {
            Level2Dash();
        }

        if (numberOfDashes >= level3DashAccord)
        {
            Level3Dash();
        }
    }
    
    private void Level1Dash()
    {
        SonAccordDash.setParameterByName("NB dash", 0f); // lancer ca quand on a fait 2 transportations sans toucher le sol
        SonAccordDash.start();
    }

    private void Level2Dash()
    {
        SonAccordDash.setParameterByName("NB dash", 2f); // lancer ca quand on a fait 5 transportations sans toucher le sol
        SonAccordDash.start();
    }

    private void Level3Dash()
    {
        SonAccordDash.setParameterByName("NB dash", 4f); // lancer ca quand on a fait 8 transportations sans toucher le sol
        SonAccordDash.start();
    }


    // --------------------------------------------  NOUVEAU ------------------------------------------------

    private void FlyingDance()
    {
        // !!!! PEUT ETRE LONG !!!!!!!!!  Son qui s'active lorsque le joueur enchaine plus de 3 transportations sans avoir touché le sol 
        RuntimeManager.PlayOneShot("event:/Player sounds/huh (effort)");
    }
}
