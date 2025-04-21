using System;
using System.Linq;
using UnityEngine;

public class AbilityShootState : AbilityState
{
    private readonly Transform playerTransform;
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
            if (reusableData.ObjectAutoAimed != null)
            {
                aimEndPosition = reusableData.ObjectAutoAimed.transform.position;
            }
            else
            {
                RaycastCheck();
            }
        }
        
        if (reusableData.LeftInput)
        {
            OnLeftShoot?.Invoke();
            leftLauncher.GetComponent<MeshRenderer>().enabled = false;
            if (reusableData.ObjectAutoAimed != null)
            {
                reusableData.LeftParent = reusableData.ObjectAutoAimed;
                reusableData.LeftObject = InstantiateBall(reusableData.LeftObject, metricsManager.CurrentMetrics.AbilityData.LeftTool, leftLauncher.position);
                reusableData.LeftObject.InitializeBall(metricsManager.CurrentMetrics.AbilityData.ShootSpeed, metricsManager.CurrentMetrics.AbilityData.TransportObjectSpeed, aimEndPosition, leftProjectileLauncher,reusableData.LeftParent, reusableData.ObjectAutoAimed);
                targetSystem.leftTargetLaunch = reusableData.ObjectAutoAimed.GetComponent<InteractiveTarget>();
            }
        }
        

        if (reusableData.RightInput)
        {
            OnRightShoot?.Invoke();
            rightLauncher.GetComponent<MeshRenderer>().enabled = false;
            if (reusableData.ObjectAutoAimed != null)
            {
                reusableData.RightParent = reusableData.ObjectAutoAimed;
                reusableData.RightObject = InstantiateBall(reusableData.RightObject, metricsManager.CurrentMetrics.AbilityData.RightTool, rightLauncher.position);
                reusableData.RightObject.InitializeBall(metricsManager.CurrentMetrics.AbilityData.ShootSpeed, metricsManager.CurrentMetrics.AbilityData.TransportObjectSpeed, aimEndPosition, rightProjectileLauncher, reusableData.RightParent, reusableData.ObjectAutoAimed);
                targetSystem.rightTargetLaunch = reusableData.ObjectAutoAimed.GetComponent<InteractiveTarget>();
            }
            
        }
        
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
        reusableData.InstancePosition = Vector3.zero;
        aimEndPosition = Vector3.zero;
        reusableData.LeftInput = false;
        reusableData.RightInput = false;
    }
    
    private void RaycastCheck()
    {
        Ray aimRay = new Ray(rigidbody.transform.position, Camera.main.transform.forward);
        RaycastHit aimHit;
        aimEndPosition = aimRay.origin + aimRay.direction * metricsManager.CurrentMetrics.AbilityData.AimDistance;
        
        if (Physics.Raycast(aimRay, out aimHit, metricsManager.CurrentMetrics.AbilityData.AimDistance, LayerMask.GetMask("Interactable"), 
                QueryTriggerInteraction.Ignore))
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
        else if (Physics.Raycast(aimRay, out aimHit, metricsManager.CurrentMetrics.AbilityData.AimDistance, ~LayerMask.GetMask("Interactable", "Player"), 
                     QueryTriggerInteraction.Ignore))
        {
            aimEndPosition = aimHit.point + aimHit.normal * 0.25f;
        }
    }
    
    private ToolManager InstantiateBall(ToolManager newInstance, ToolManager refObject, Vector3 defaultPosition)
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
