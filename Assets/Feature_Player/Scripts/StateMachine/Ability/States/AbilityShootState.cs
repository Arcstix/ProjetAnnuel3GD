using UnityEngine;

public class AbilityShootState : AbilityState
{
    private Transform playerTransform;

    private Vector3 aimEndPosition;
    public AbilityShootState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
        playerTransform = _stateMachine.AbilityManager.transform;
    }

    public override void Enter()
    {
        base.Enter();
        
        // TODO : Method Shoot mettre la balle à l'endroit désigné et si pas d'endroit désigné tiré un Raycast
        
        if (reusableData.LeftInput)
        {
            leftLauncher.GetComponent<MeshRenderer>().enabled = false;
            reusableData.LeftObject = InstantiateBall(reusableData.LeftObject, metricsManager.CurrentPlayerSO.AbilityData.LeftBall, leftLauncher.position);
        }
        

        if (reusableData.RightInput)
        {
            rightLauncher.GetComponent<MeshRenderer>().enabled = false;
            reusableData.RightObject = InstantiateBall(reusableData.RightObject, metricsManager.CurrentPlayerSO.AbilityData.RightBall, rightLauncher.position);
        }

        if (reusableData.InstancePosition != Vector3.zero)
        {
            aimEndPosition = reusableData.InstancePosition;
        }
        else
        {
            RaycastCheck();
        }
    }

    public override void Tick()
    {
        base.Tick();

        Debug.Log("position : " + reusableData.InstancePosition);
        
        if (reusableData.LeftInput)
        {
            reusableData.LeftObject.transform.position = Vector3.Lerp(reusableData.LeftObject.transform.position,
                aimEndPosition, Time.deltaTime * metricsManager.CurrentPlayerSO.AbilityData.ShootSpeed);

            if (Vector3.Distance(reusableData.LeftObject.transform.position, aimEndPosition) < 0.1f)
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }
        }
        
        if (reusableData.RightInput)
        {
            reusableData.RightObject.transform.position = Vector3.Lerp(reusableData.RightObject.transform.position,
                aimEndPosition, Time.deltaTime * metricsManager.CurrentPlayerSO.AbilityData.ShootSpeed);

            if (Vector3.Distance(reusableData.RightObject.transform.position, aimEndPosition) < 0.1f)
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
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
    
    private void RaycastCheck()
    {
        Ray aimRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit aimHit;
        aimEndPosition = aimRay.origin + aimRay.direction * metricsManager.CurrentPlayerSO.AbilityData.AimDistance;
        
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out aimHit,
                metricsManager.CurrentPlayerSO.AbilityData.AimDistance, LayerMask.GetMask("Interactable")))
        {
            aimEndPosition = aimHit.point;
        }
        else if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out aimHit,
                     metricsManager.CurrentPlayerSO.AbilityData.AimDistance, ~LayerMask.GetMask("Interactable")))
        {
            aimEndPosition = aimHit.point + aimHit.normal * 0.25f;
        }
    }
    
    private GameObject InstantiateBall(GameObject newInstance, GameObject refObject, Vector3 defaultPosition)
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
