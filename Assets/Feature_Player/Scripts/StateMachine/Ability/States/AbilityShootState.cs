using UnityEngine;

public class AbilityShootState : AbilityState
{
    private Transform playerTransform;
    private Transform launchTransform;
    public AbilityShootState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
        playerTransform = _stateMachine.AbilityManager.transform;
        launchTransform = _stateMachine.AbilityManager.launcherTransform;
    }

    public override void Enter()
    {
        base.Enter();
        
        // TODO : Method Shoot mettre la balle à l'endroit désigné et si pas d'endroit désigné tiré un Raycast
        
        if (reusableData.LeftInput)
        {
            reusableData.LeftObject = InstantiateBall(reusableData.LeftObject, metricsManager.CurrentPlayerSO.AbilityData.LeftBall);
            Debug.Log(reusableData.LeftObject);
        }
        

        if (reusableData.RightInput)
        {
            reusableData.RightObject = InstantiateBall(reusableData.RightObject, metricsManager.CurrentPlayerSO.AbilityData.RightBall);
            Debug.Log(reusableData.RightObject);
        }
    }

    public override void Tick()
    {
        base.Tick();

        if (reusableData.LeftInput)
        {
            if (reusableData.InstancePosition != Vector3.zero)
            {
                reusableData.LeftObject.transform.position = Vector3.Lerp(reusableData.LeftObject.transform.position,
                    reusableData.InstancePosition, Time.deltaTime * 10f);

                if (Vector3.Distance(reusableData.LeftObject.transform.position, reusableData.InstancePosition) < 0.1f)
                {
                    _stateMachine.ChangeState(_stateMachine.IdleState);
                }
            }
            else
            {
                Ray aimRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                Vector3 aimEndPosition = aimRay.origin + aimRay.direction * metricsManager.CurrentPlayerSO.AbilityData.AimDistance;
                
                reusableData.LeftObject.transform.position = Vector3.Lerp(reusableData.LeftObject.transform.position,
                    aimEndPosition, Time.deltaTime * 10f);

                if (Vector3.Distance(reusableData.LeftObject.transform.position, aimEndPosition) < 0.1f)
                {
                    _stateMachine.ChangeState(_stateMachine.IdleState);
                }
            }
        }
        
        if (reusableData.RightInput)
        {
            if (reusableData.InstancePosition != Vector3.zero)
            {
                reusableData.RightObject.transform.position = Vector3.Lerp(reusableData.RightObject.transform.position,
                    reusableData.InstancePosition, Time.deltaTime * 10f);

                if (Vector3.Distance(reusableData.RightObject.transform.position, reusableData.InstancePosition) < 0.1f)
                {
                    _stateMachine.ChangeState(_stateMachine.IdleState);
                }
            }
            else
            {
                Ray aimRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                Vector3 aimEndPosition = aimRay.origin + aimRay.direction * metricsManager.CurrentPlayerSO.AbilityData.AimDistance;
                
                reusableData.RightObject.transform.position = Vector3.Lerp(reusableData.RightObject.transform.position,
                    aimEndPosition, Time.deltaTime * 10f);

                if (Vector3.Distance(reusableData.RightObject.transform.position, aimEndPosition) < 0.1f)
                {
                    _stateMachine.ChangeState(_stateMachine.IdleState);
                }
            }
        }

        
    }

    public override void Exit()
    {
        base.Exit();
        reusableData.InstancePosition = Vector3.zero;
        reusableData.LeftInput = false;
        reusableData.RightInput = false;
    }
    
    private GameObject InstantiateBall(GameObject newInstance, GameObject refObject)
    {
        if (CheckWalls())
        {
            newInstance = GameObject.Instantiate(refObject,
                reusableData.InstancePosition, Quaternion.identity);
        }
        else
        {
            newInstance = GameObject.Instantiate(refObject,
                launchTransform.position, Quaternion.identity);
        }
        
        return newInstance;
    }
    
    // Check si on est contre un objet
    private bool CheckWalls()
    {
        float distance = Vector3.Distance(launchTransform.position, playerTransform.position);
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
