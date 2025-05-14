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

    private void Awake()
    {
        abilityManager = GetComponent<PlayerAbilityManager>();
        abilityManager.OnAbilityStarted += InitAbility;
    }

    private void Start()
    {
        SonAccordDash = RuntimeManager.CreateInstance("event:/Musique/Accords dash");
    }

    private void InitAbility()
    {
        abilityManager.AbilityStateMachine.ShootState.OnRightShoot += RightShoot;
        abilityManager.AbilityStateMachine.ShootState.OnLeftShoot += LeftShoot;
        abilityManager.AbilityStateMachine.RecallState.OnRightRecall += RightRecall;
        abilityManager.AbilityStateMachine.RecallState.OnLeftRecall += LeftRecall;
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
        abilityManager.AbilityStateMachine.RecallState.OnRightRecall -= RightRecall;
        abilityManager.AbilityStateMachine.RecallState.OnLeftRecall -= LeftRecall;
        abilityManager.AbilityStateMachine.MovePlayer.EnterDash -= Dash;
        abilityManager.AbilityStateMachine.MovePlayer.OnFlyingDance -= FlyingDance;
    }

    private void Update()
    {
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
        RuntimeManager.PlayOneShot("event:/Player/TIr droit");
    }

    private void LeftShoot()
    {
        //Son qui se d�clenche lorsqu'on tire la balle gauche
        RuntimeManager.PlayOneShot("event:/Player/Tir gauche");
    }
    
    private void RightRecall()
    {
        //Son qui se d�clenche lorsqu'on rappelle la balle droite
        RuntimeManager.PlayOneShot("event:/Player/rappel droit");
    }
    
    private void LeftRecall()
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
