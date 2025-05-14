using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private PlayerAbilityManager abilityManager;
    
    private PlayerMetricsManager metricsManager;
    private HealthPlayer playerHealth;
    

    private void Awake()
    {
        abilityManager = GetComponent<PlayerAbilityManager>();
        metricsManager = GetComponent<PlayerMetricsManager>();
        playerHealth = GetComponent<HealthPlayer>();
        abilityManager.OnAbilityStarted += InitAbility;
    }

    private void Start()
    {
        metricsManager.OnRightChargeReady += RightChargeReady;
        metricsManager.OnLeftChargeReady += LeftChargeReady;
        metricsManager.OnNoCharge += NoCharge;
        playerHealth.OnDanger += DangerSound;
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
        metricsManager.OnRightChargeReady -= RightChargeReady;
        metricsManager.OnLeftChargeReady -= LeftChargeReady;
        metricsManager.OnNoCharge -= NoCharge;
        playerHealth.OnDanger -= DangerSound;
        
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
    

    public void FlyingDance()
    {
        // !!!! PEUT ETRE LONG !!!!!!!!!  Son qui s'active lorsque le joueur enchaine plus de 3 transportations sans avoir touché le sol 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/huh (effort)");
    }

    public void DangerSound()
    {
        // !!!! PEUT ETRE LONG !!!!!!!!! Son qui s'active lorsque la barre de vie du joueur ( la jauge de vision ennemis ) atteint 30%
        Debug.Log("Danger !!!!");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/ah (degats)");
    }
}
