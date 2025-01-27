using System;
using UnityEngine;

public class AbilityShootState : AbilityState
{
    private Transform playerTransform;
    private Vector3 aimEndPosition;

    public event Action OnRightShoot;
    public event Action OnLeftShoot;
    
    public AbilityShootState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
        playerTransform = _stateMachine.AbilityManager.transform;
    }

    public override void Enter()
    {
        base.Enter();
        if (reusableData.InstancePosition != Vector3.zero)
        {
            aimEndPosition = reusableData.InstancePosition;
        }
        else
        {
            RaycastCheck();
        }
        
        if (reusableData.LeftInput)
        {
            OnLeftShoot?.Invoke();
            leftLauncher.GetComponent<MeshRenderer>().enabled = false;
            reusableData.LeftObject = InstantiateBall(reusableData.LeftObject, metricsManager.CurrentPlayerSO.AbilityData.LeftBall, leftLauncher.position);
            reusableData.LeftObject.InitializeBall(metricsManager.CurrentPlayerSO.AbilityData.ShootSpeed, aimEndPosition, reusableData.LeftParent);
        }
        

        if (reusableData.RightInput)
        {
            OnRightShoot?.Invoke();
            rightLauncher.GetComponent<MeshRenderer>().enabled = false;
            reusableData.RightObject = InstantiateBall(reusableData.RightObject, metricsManager.CurrentPlayerSO.AbilityData.RightBall, rightLauncher.position);
            reusableData.RightObject.InitializeBall(metricsManager.CurrentPlayerSO.AbilityData.ShootSpeed, aimEndPosition, reusableData.RightParent);
        }
        
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
        reusableData.InstancePosition = Vector3.zero;
        reusableData.LeftInput = false;
        reusableData.RightInput = false;
    }
    
    private void RaycastCheck()
    {
        Ray aimRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit aimHit;
        aimEndPosition = aimRay.origin + aimRay.direction * metricsManager.CurrentPlayerSO.AbilityData.AimDistance;
        
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out aimHit,
                metricsManager.CurrentPlayerSO.AbilityData.AimDistance, LayerMask.GetMask("Interactable"), QueryTriggerInteraction.Ignore))
        {
            aimEndPosition = aimHit.point;
            if (reusableData.LeftInput)
            {
                reusableData.LeftParent = aimHit.transform.gameObject;
            }
            else
            {
                reusableData.RightParent = aimHit.transform.gameObject;
            }
        }
        else if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out aimHit,
                     metricsManager.CurrentPlayerSO.AbilityData.AimDistance, ~LayerMask.GetMask("Interactable")))
        {
            aimEndPosition = aimHit.point + aimHit.normal * 0.25f;
        }
    }
    
    private BallManager InstantiateBall(BallManager newInstance, BallManager refObject, Vector3 defaultPosition)
    {
        if (CheckWalls(defaultPosition))
        {
            newInstance = GameObject.Instantiate(refObject,
                aimEndPosition, Quaternion.identity);
        }
        else
        {
            newInstance = GameObject.Instantiate(refObject,
                defaultPosition, Quaternion.identity);
        }
        
        return newInstance;
    }
    
    // Check si on est contre un objet
    private bool CheckWalls(Vector3 defaultPosition)
    {
        float distance = Vector3.Distance(defaultPosition, playerTransform.position);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit,
                distance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
