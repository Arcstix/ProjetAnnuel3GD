using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityAimState : AbilityState
{
    private Transform playerTransform;
    private GameObject aimObject;
    private Vector3 velocityRef;
    public AbilityAimState(AbilityStateMachine abilityStateMachine) : base(abilityStateMachine)
    {
        playerTransform = metricsManager.gameObject.transform;
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("AimState Enter");
    }

    public override void Tick()
    {
        base.Tick();
        
        if (input.PlayerActions.ThrowRecallRight.WasReleasedThisFrame())
        {
            HandleRightShootRecall();
            return;
        }

        if (input.PlayerActions.ThrowRecallLeft.WasReleasedThisFrame())
        {
            HandleLeftShootRecall();
            return;
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
        
        GameObject.Destroy(aimObject);
    }

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
            Debug.Log("AimObject is null");
            aimObject = GameObject.Instantiate(metricsManager.CurrentPlayerSO.AbilityData.AimBall, aimEndPosition, Quaternion.identity);
        }
        
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out aimHit,
                metricsManager.CurrentPlayerSO.AbilityData.AimDistance, LayerMask.GetMask("Interactable")))
        {
            Debug.Log("Can Interact");
            aimObject.GetComponent<MeshRenderer>().material.color = Color.green;
            aimObject.transform.position = Vector3.Lerp(aimObject.transform.position, aimHit.point, Time.deltaTime * smoothSpeed);
        }
        else if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out aimHit,
                     metricsManager.CurrentPlayerSO.AbilityData.AimDistance, ~LayerMask.GetMask("Interactable")))
        {
            Debug.Log("Cannot Interact");
            aimObject.GetComponent<MeshRenderer>().material.color = Color.grey;
            aimObject.transform.position = Vector3.Lerp(aimObject.transform.position, aimHit.point + aimHit.normal * aimObject.transform.localScale.x, Time.deltaTime * smoothSpeed);
        }
        else
        {
            aimObject.GetComponent<MeshRenderer>().material.color = Color.grey;
            aimObject.transform.position = Vector3.Lerp(aimObject.transform.position, aimEndPosition, Time.deltaTime * smoothSpeed);
        }
        
        reusableData.InstancePosition = aimObject.transform.position;
        // Si le Raycast touche un objet intéragissable le point de contact est sur l'objet (planté)
        // Si le Raycast touche un objet non intéragissable le visuel se trouve à un offset de l'objet (non planté)
        // Si le Raycast ne touche rien le visuel se trouve au bout du Raycast
    }
}
