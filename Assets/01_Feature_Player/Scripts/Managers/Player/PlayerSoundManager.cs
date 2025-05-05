using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private PlayerAbilityManager abilityManager;
    private PlayerMovementManager movementManager;

    private void Awake()
    {
        abilityManager = GetComponent<PlayerAbilityManager>();
        movementManager = GetComponent<PlayerMovementManager>();
        abilityManager.OnAbilityStarted += InitAbility;
        movementManager.OnMovementStarted += InitMovement;
    }

    private void InitMovement()
    {
        movementManager.StateMachine.LandingState.OnLanding += PlayerLanding;
    }

    public void InitAbility()
    {
        abilityManager.AbilityStateMachine.ShootState.OnRightShoot += RightShoot;
        abilityManager.AbilityStateMachine.ShootState.OnLeftShoot += LeftShoot;
        abilityManager.AbilityStateMachine.RecallState.OnRightRecall += RightRecall;
        abilityManager.AbilityStateMachine.RecallState.OnLeftRecall += LeftRecall;
        abilityManager.AbilityStateMachine.MoveLeftObject.OnRightActivation += RightActivation;
        abilityManager.AbilityStateMachine.MoveRightObject.OnLeftActivation += LeftActivation;
        abilityManager.AbilityStateMachine.MovePlayer.EnterDash += Dash;
        abilityManager.AbilityStateMachine.AimState.OnAirAim += EnterGravityFreeze;
        abilityManager.AbilityStateMachine.AimState.OnExitAim += QuitGravityFreeze;
    }

    private void OnDisable()
    {
        abilityManager.AbilityStateMachine.ShootState.OnRightShoot -= RightShoot;
        abilityManager.AbilityStateMachine.ShootState.OnLeftShoot -= LeftShoot;
        abilityManager.AbilityStateMachine.RecallState.OnRightRecall -= RightRecall;
        abilityManager.AbilityStateMachine.RecallState.OnLeftRecall -= LeftRecall;
        abilityManager.AbilityStateMachine.MoveLeftObject.OnRightActivation -= RightActivation;
        abilityManager.AbilityStateMachine.MoveRightObject.OnLeftActivation -= LeftActivation;
        abilityManager.AbilityStateMachine.MovePlayer.EnterDash -= Dash;
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

    public void RightActivation()
    {
        //Son qui se d�clenche lorsqu'on active la balle droite
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/activation droit");
    }
    
    public void LeftActivation()
    {
        //Son qui se d�clenche lorsqu'on active la balle gauche
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/activation gauche");
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
        //  Son qui s'active � la fin du gravity freeze
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Chute");
    }

    public void PlayerLanding()
    {
        // Son qui s'active lorsque le joueur touche le sol. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/player collision");
    }


    // --------------------------------------------  NOUVEAU ------------------------------------------------

    public void StaminaReady()
    {
        // Son qui s'active lorsqu'une charge de stamina est pleine. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/No Stamina");
    }

    public void NoStamina()
    {
        // Son qui s'active lorsque le joueur veut s'attirer à un outil mais qu'il n'a pas encore récupéré sa stamina. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/UI - stamina ready");
    }

    public void Jump()
    {
        // Son qui s'active lorsque le joueur veut s'attirer à un outil mais qu'il n'a pas encore récupéré sa stamina. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/UI - stamina ready");
    }

    public void DoubleJump()
    {
        // Son qui s'active lorsque le joueur veut s'attirer à un outil mais qu'il n'a pas encore récupéré sa stamina. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/UI - stamina ready");
    }

    public void BruitPasRoche()
    {
        // Son qui s'active lorsque le joueur veut s'attirer à un outil mais qu'il n'a pas encore récupéré sa stamina. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/UI - stamina ready");
    }

    public void BruitPasSable()
    {
        // Son qui s'active lorsque le joueur veut s'attirer à un outil mais qu'il n'a pas encore récupéré sa stamina. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/UI - stamina ready");
    }

    public void WallRun()
    {
        // Son qui s'active lorsque le joueur veut s'attirer à un outil mais qu'il n'a pas encore récupéré sa stamina. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/UI - stamina ready");
    }


}
