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
        //  Son qui s'active � la fin du gravity freeze (Chute)
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Chute");
    }

    public void PlayerLanding()
    {
        // Son qui s'active lorsque le joueur touche le sol. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/player collision");
    }


    // --------------------------------------------  NOUVEAU ------------------------------------------------

    public void StaminaReadyDroite()
    {
        // Son qui s'active lorsque la charge de stamina droite est pleine. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/UI - stamina ready D");
    }

    public void StaminaReadyGauche()
    {
        // Son qui s'active lorsque la charge de stamina gauche est pleine. 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/UI - stamina ready G");
    }

    public void NoStamina()
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

    public void BruitPasRoche()
    {
        // !!!!!!!! ON VERRA !!!!!!!! Son qui s'active lorsque le joueur marche au sol dans la zone grotte 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/Marche roche");
    }

    public void BruitPasSable()
    {
        // Son qui s'active lorsque le joueur marche au sol ... 
        // EN GROS il faudrait que tu crées une vriable walkSpeed qui puisse se modifier (0.5 par exemple), et cette fonction BruitPasSable
        // est appelée (tous les 0.5 secondes dans ce cas) quand le joueur est en etat "walk", pour simuler des bruits de marche
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/Marche sable");
    }

    public void WallRun()
    {
        // Son qui s'active lorsque le joueur se déplace sur un wall run (est en état wall run) 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/Wall Run");
    }

    public void CollisionSolFaible()
    {
        // Son qui s'active lorsque le joueur collisionne le sol
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/player collision sol faible");
    }

    public void CollisionSolForte()
    {
        // !!!!!!! ON VERRA !!!!!!!!! Son qui s'active lorsque le joueur collisionne le sol avec une vitesse et hauteur importante 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/player collision sol fort");
    }

    public void SonEssouflement()
    {
        // !!!! PEUT ETRE LONG !!!!!!!!! Son qui s'active lorsque le joueur atterri sur un sol avec ses deux charges vides 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/Essouflement");
    }

    public void SonEffort()
    {
        // !!!! PEUT ETRE LONG !!!!!!!!!  Son qui s'active lorsque le joueur enchaine plus de 3 transportations sans avoir touché le sol 
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/huh (effort)");
    }

    public void SonDouleur()
    {
        // !!!! PEUT ETRE LONG !!!!!!!!! Son qui s'active lorsque la barre de vie du joueur ( la jauge de vision ennemis ) atteint 30%
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player sounds/ah (degats)");
    }
}
