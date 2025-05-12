using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private float footstepInterval = 0.35f;
    
    private PlayerAbilityManager abilityManager;
    private PlayerMovementManager movementManager;
    private PlayerMetricsManager metricsManager;
    private HealthPlayer playerHealth;
    private GroundCheck groundCheck;

    private void Awake()
    {
        abilityManager = GetComponent<PlayerAbilityManager>();
        movementManager = GetComponent<PlayerMovementManager>();
        metricsManager = GetComponent<PlayerMetricsManager>();
        playerHealth = GetComponent<HealthPlayer>();
        groundCheck = GetComponent<GroundCheck>();
        abilityManager.OnAbilityStarted += InitAbility;
        movementManager.OnMovementStarted += InitMovement;
    }

    private void Start()
    {
        metricsManager.OnRightStaminaReady += RightChargeReady;
        metricsManager.OnLeftStaminaReady += LeftChargeReady;
        metricsManager.OnNoCharge += NoCharge;
        playerHealth.OnDanger += DangerSound;
    }

    private void InitMovement()
    {
        movementManager.StateMachine.JumpState.OnJump += Jump;
        movementManager.StateMachine.JumpState.OnDoubleJump += DoubleJump;
        movementManager.StateMachine.RunningState.OnRunning += StartRunning;
        movementManager.StateMachine.RunningState.OnStopRunning += StopRunning;
        movementManager.StateMachine.WallRunState.OnWallRun += StartWallRunning;
        movementManager.StateMachine.WallRunState.ExitWallRun += StopWallRun;
        movementManager.StateMachine.SoftLandingState.OnSoftLanding += SoftLanding;
        movementManager.StateMachine.HardLandingState.OnHardLanding += HardLanding;
        movementManager.StateMachine.SoftLandingState.OnNoStaminaLanding += NoStaminaLanding;
        movementManager.StateMachine.HardLandingState.OnNoStaminaLanding += NoStaminaLanding;
    }

    public void InitAbility()
    {
        abilityManager.AbilityStateMachine.ShootState.OnRightShoot += RightShoot;
        abilityManager.AbilityStateMachine.ShootState.OnLeftShoot += LeftShoot;
        abilityManager.AbilityStateMachine.RecallState.OnRightRecall += RightRecall;
        abilityManager.AbilityStateMachine.RecallState.OnLeftRecall += LeftRecall;
        abilityManager.AbilityStateMachine.MovePlayer.EnterDash += Dash;
        abilityManager.AbilityStateMachine.AimState.OnAirAim += EnterGravityFreeze;
        abilityManager.AbilityStateMachine.AimState.OnExitAim += QuitGravityFreeze;
        abilityManager.AbilityStateMachine.MovePlayer.OnFlyingDance += FlyingDance;
    }

    private void OnDisable()
    {
        metricsManager.OnRightStaminaReady -= RightChargeReady;
        metricsManager.OnLeftStaminaReady -= LeftChargeReady;
        metricsManager.OnNoCharge -= NoCharge;
        playerHealth.OnDanger -= DangerSound;
        
        movementManager.StateMachine.JumpState.OnJump -= Jump;
        movementManager.StateMachine.JumpState.OnDoubleJump -= DoubleJump;
        movementManager.StateMachine.RunningState.OnRunning -= StartRunning;
        movementManager.StateMachine.RunningState.OnStopRunning -= StopRunning;
        movementManager.StateMachine.WallRunState.OnWallRun -= StartWallRunning;
        movementManager.StateMachine.WallRunState.ExitWallRun -= StopWallRun;
        movementManager.StateMachine.SoftLandingState.OnSoftLanding -= SoftLanding;
        movementManager.StateMachine.HardLandingState.OnHardLanding -= HardLanding;
        movementManager.StateMachine.SoftLandingState.OnNoStaminaLanding -= NoStaminaLanding;
        movementManager.StateMachine.HardLandingState.OnNoStaminaLanding -= NoStaminaLanding;
        
        abilityManager.AbilityStateMachine.ShootState.OnRightShoot -= RightShoot;
        abilityManager.AbilityStateMachine.ShootState.OnLeftShoot -= LeftShoot;
        abilityManager.AbilityStateMachine.RecallState.OnRightRecall -= RightRecall;
        abilityManager.AbilityStateMachine.RecallState.OnLeftRecall -= LeftRecall;
        abilityManager.AbilityStateMachine.MovePlayer.EnterDash -= Dash;
        abilityManager.AbilityStateMachine.MovePlayer.OnFlyingDance -= FlyingDance;
    }

    public void RightShoot()
    {
        //Son qui se d�clenche lorsqu'on tire la balle droite
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/TIr droit");
    }

    public void LeftShoot()
    {
        //Son qui se d�clenche lorsqu'on tire la balle gauche
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Tir gauche");
    }
    
    public void RightRecall()
    {
        //Son qui se d�clenche lorsqu'on rappelle la balle droite
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/rappel droit");
    }
    
    public void LeftRecall()
    {
        //Son qui se d�clenche lorsqu'on rappelle la balle gauche
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/rappel gauche");
    }
    
    public void Dash()
    {
        // Son lorsqu'on active un outil pour dash vers lui
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/dash");
    }
    
    public void EnterGravityFreeze()
    {
        //  Son lorsqu"on rentre en etat gravity Freeze
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/gravity freeze");
    }
    
    public void QuitGravityFreeze()
    {
        //  Son qui s'active � la fin du gravity freeze (Chute)
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Chute");
    }


    // --------------------------------------------  NOUVEAU ------------------------------------------------

    public void RightChargeReady()
    {
        // Son qui s'active lorsque la charge de stamina droite est pleine. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/UI - stamina ready D");
    }

    public void LeftChargeReady()
    {
        // Son qui s'active lorsque la charge de stamina gauche est pleine. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/UI - stamina ready G");
    }

    public void NoCharge()
    {
        // Son qui s'active lorsque le joueur veut s'attirer à un outil mais qu'il n'a pas encore récupéré sa stamina. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/No Stamina");
    }

    public void Jump()
    {
        // Son qui s'active lorsque le joueur saute.
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/Jump");
    }

    public void DoubleJump()
    {
        // Son qui s'active lorsque le joueur utilise son deuxieme saut. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/Double jump");
    }

    public void RockFootstep()
    {
        // !!!!!!!! ON VERRA !!!!!!!! Son qui s'active lorsque le joueur marche au sol dans la zone grotte 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/Marche roche");
    }

    public void SableFootstep()
    {
        // Son qui s'active lorsque le joueur marche au sol ... 
        // EN GROS il faudrait que tu crées une variable walkSpeed qui puisse se modifier (0.5 par exemple), et cette fonction BruitPasSable
        // est appelée (tous les 0.5 secondes dans ce cas) quand le joueur est en etat "walk", pour simuler des bruits de marche
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/Marche sable");
    }
    
    private void StopRunning()
    {
        CancelInvoke();
    }

    private void StopWallRun()
    {
        CancelInvoke();
    }

    private void StartRunning()
    {
        if (groundCheck)
        {
            InvokeRepeating(nameof(PlayRunningSound), 0, footstepInterval);
        }
    }

    private void StartWallRunning(float value)
    {
        InvokeRepeating(nameof(WallRun), 0, footstepInterval);
    }

    private void PlayRunningSound()
    {
        switch (groundCheck.DetectSurface())
        {
            case GroundTypeEnum.None:
                RockFootstep();
                break;
            case GroundTypeEnum.Rock:
                RockFootstep();
                break;
            case GroundTypeEnum.Sable:
                SableFootstep();
                break;
        }
    }

    public void WallRun()
    {
        // Son qui s'active lorsque le joueur se déplace sur un wall run (est en état wall run) 
        // Fonctionnement comme la marche  
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/Wall Run");
    }

    public void SoftLanding()
    {
        // Son qui s'active lorsque le joueur collisionne le sol
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/player collision sol faible");
    }

    public void HardLanding()
    {
        // !!!!!!! ON VERRA !!!!!!!!! Son qui s'active lorsque le joueur collisionne le sol avec une vitesse et hauteur importante 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/player collision sol fort");
    }

    public void NoStaminaLanding()
    {
        // !!!! PEUT ETRE LONG !!!!!!!!! Son qui s'active lorsque le joueur atterri sur un sol avec ses deux charges vides 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/Essouflement");
    }

    public void FlyingDance()
    {
        // !!!! PEUT ETRE LONG !!!!!!!!!  Son qui s'active lorsque le joueur enchaine plus de 3 transportations sans avoir touché le sol 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/huh (effort)");
    }

    public void DangerSound()
    {
        // !!!! PEUT ETRE LONG !!!!!!!!! Son qui s'active lorsque la barre de vie du joueur ( la jauge de vision ennemis ) atteint 30%
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/ah (degats)");
    }
}
