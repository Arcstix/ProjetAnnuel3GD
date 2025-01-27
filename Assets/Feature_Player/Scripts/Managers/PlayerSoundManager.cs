using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour, I_Initializer
{
    private PlayerAbilityManager abilityManager;
    private PlayerMovementManager movementManager;
    
    public void Init(PlayerReusableStateData reusableStateData)
    {
        abilityManager = GetComponent<PlayerAbilityManager>();

        abilityManager.AbilityStateMachine.ShootState.OnRightShoot += RightShoot;
        abilityManager.AbilityStateMachine.ShootState.OnLeftShoot += LeftShoot;
        abilityManager.AbilityStateMachine.RecallState.OnRightRecall += RightRecall;
        abilityManager.AbilityStateMachine.RecallState.OnLeftRecall += LeftRecall;
        abilityManager.AbilityStateMachine.TransportState.OnRightActivation += RightActivation;
        abilityManager.AbilityStateMachine.TransportState.OnLeftActivation += LeftActivation;
        abilityManager.AbilityStateMachine.TransportState.OnDash += Dash;
        
        movementManager = GetComponent<PlayerMovementManager>();

        movementManager.MovementStateMachine.FallingState.OnGravityFreeze += GravityFreeze;
        movementManager.MovementStateMachine.FallingState.OnGravityUnFreeze += Falling;
    }

    private void OnDisable()
    {
        abilityManager.AbilityStateMachine.ShootState.OnRightShoot -= RightShoot;
        abilityManager.AbilityStateMachine.ShootState.OnLeftShoot -= LeftShoot;
        abilityManager.AbilityStateMachine.RecallState.OnRightRecall -= RightRecall;
        abilityManager.AbilityStateMachine.RecallState.OnLeftRecall -= LeftRecall;
        abilityManager.AbilityStateMachine.TransportState.OnRightActivation -= RightActivation;
        abilityManager.AbilityStateMachine.TransportState.OnLeftActivation -= LeftActivation;
        abilityManager.AbilityStateMachine.TransportState.OnDash -= Dash;
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
    
    public void GravityFreeze()
    {
        //  Son lorsqu"on rentre en etat gravity Freeze
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/gravity freeze");
    }
    
    public void Falling()
    {
        //  Son qui s'active � la fin du gravity freeze
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Chute");
    }

    public void Collision()
    {
        //  !!! JE DEVRAI SUREMENT LE FAIRE MOI MEME CELUI CI !!!  Son qui s'activee qaund le joueur entre en collision avec quelque chose
    }
}
