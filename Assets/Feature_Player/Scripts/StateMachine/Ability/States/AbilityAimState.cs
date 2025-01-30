using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityAimState : AbilityState
{
    private GameObject aimObject;
    private Vector3 velocityRef;
    
    public event Action OnAim;
    public event Action OnRelease;
    public AbilityAimState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
        
    }
    
    #region StateMachine

    public override void Enter()
    {
        base.Enter();
        
        OnAim?.Invoke();
        
        if (reusableData.RightInput)
        {
            rightLauncher.localPosition = Vector3.Lerp(rightLauncher.localPosition, rightLauncher.localPosition + metricsManager.CurrentPlayerSO.AbilityData.AimBallOffset, 0.5f);
        }

        if (reusableData.LeftInput)
        {
            leftLauncher.localPosition = Vector3.Lerp(leftLauncher.localPosition, leftLauncher.localPosition + metricsManager.CurrentPlayerSO.AbilityData.AimBallOffset, 0.5f);
        }
    }

    public override void Tick()
    {
        base.Tick();
        
        if (reusableData.RightInput)
        {
            if (input.PlayerActions.ThrowRecallRight.WasReleasedThisFrame())
            {
                HandleRightShootRecall();
                return;
            }
        }
        
        if (reusableData.LeftInput)
        {
            if (input.PlayerActions.ThrowRecallLeft.WasReleasedThisFrame())
            {
                HandleLeftShootRecall();
                return;
            }
        }
    }

    public override void FixedTick()
    {
        base.FixedTick();
        
        ShowAiming();
    }

    public override void Exit()
    {
        base.Exit();
        
        OnRelease?.Invoke();
        
        GameObject.Destroy(aimObject);

        if (reusableData.ObjectAimed != null)
        {
            if (reusableData.RightInput)
            {
                reusableData.RightParent = reusableData.ObjectAimed;
            }
            else
            {
                reusableData.LeftParent = reusableData.ObjectAimed;
            }
        }
        
        if (reusableData.RightInput)
        {
            rightLauncher.localPosition = Vector3.Lerp(rightLauncher.localPosition, rightLauncher.localPosition - metricsManager.CurrentPlayerSO.AbilityData.AimBallOffset, 0.5f);
        }

        if (reusableData.LeftInput)
        {
            leftLauncher.localPosition = Vector3.Lerp(leftLauncher.localPosition, leftLauncher.localPosition - metricsManager.CurrentPlayerSO.AbilityData.AimBallOffset, 0.5f);
        }

    }
    #endregion
    
    // Affiche une prévisualisation de la balle 
    private void ShowAiming()
    {
        // Tir un Raycast d'une certaine longueur
        Ray aimRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit aimHit;
        Vector3 aimEndPosition = aimRay.origin + aimRay.direction * metricsManager.CurrentPlayerSO.AbilityData.AimDistance;
        float smoothSpeed = metricsManager.CurrentPlayerSO.AbilityData.AimSpeed;
        
        if (aimObject == null)
        {
            aimObject = GameObject.Instantiate(metricsManager.CurrentPlayerSO.AbilityData.AimBall, aimEndPosition, Quaternion.identity);
        }
        
        // Si le Raycast touche un objet intéragissable le point de contact est sur l'objet (planté)
        // Si le Raycast touche un objet non intéragissable le visuel se trouve à un offset de l'objet (non planté)
        // Si le Raycast ne touche rien le visuel se trouve au bout du Raycast
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out aimHit,
                metricsManager.CurrentPlayerSO.AbilityData.AimDistance, LayerMask.GetMask("Interactable"), QueryTriggerInteraction.Ignore))
        {
            aimObject.GetComponent<MeshRenderer>().material.color = Color.green;
            aimObject.transform.position = Vector3.Lerp(aimObject.transform.position, aimHit.point, Time.unscaledDeltaTime * smoothSpeed);
            reusableData.ObjectAimed = aimHit.collider.gameObject;
        }
        else if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out aimHit,
                     metricsManager.CurrentPlayerSO.AbilityData.AimDistance, ~LayerMask.GetMask("Interactable")))
        {
            aimObject.GetComponent<MeshRenderer>().material.color = Color.grey;
            aimObject.transform.position = Vector3.Lerp(aimObject.transform.position, aimHit.point + aimHit.normal * aimObject.transform.localScale.x, Time.unscaledDeltaTime * smoothSpeed);
            reusableData.ObjectAimed = null;
        }
        else
        {
            aimObject.GetComponent<MeshRenderer>().material.color = Color.grey;
            aimObject.transform.position = Vector3.Lerp(aimObject.transform.position, aimEndPosition, Time.unscaledDeltaTime * smoothSpeed);
            reusableData.ObjectAimed = null;
        }
        
        reusableData.InstancePosition = aimObject.transform.position;
    }
}
