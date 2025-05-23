using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

// S'occupe de mettre � jour les param�tre de la cam�ra 
public class PlayerCameraManager : MonoBehaviour, I_Initializer
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] [Range(0,20)] private float controllerSpeed;
    [SerializeField] [Range(0,20)] private float mouseSpeed;

    private PlayerMetricsManager metricsManager;
    private PlayerAbilityManager abilityManager;
    private PlayerMovementManager movementManager;
    private PlayerUIManager uiManager;
    private PlayerInput playerInput;
    private CinemachineFramingTransposer framingTransposer;
    private float currentTargetDistance;
    private PlayerCameraData cameraData;
    
    private InputAction freeLookAction;
    private string lastUsedDevice = "";
    private float currentFOV = 0f;
    private float targetFOV;
    private float targetDutch;
    

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        uiManager = GetComponent<PlayerUIManager>();
    }

    public void Init(PlayerReusableStateData reusableStateData)
    {
        metricsManager = GetComponent<PlayerMetricsManager>();
        abilityManager = GetComponent<PlayerAbilityManager>();
        movementManager = GetComponent<PlayerMovementManager>();
        abilityManager.AbilityStateMachine.MovePlayer.SpeedModifierEvent += SetTransportFOV;
        abilityManager.AbilityStateMachine.MovePlayer.ExitDash += SetBaseFOV;
        movementManager.StateMachine.WallRunState.OnWallRun += WallRunRotate;
        movementManager.StateMachine.WallRunState.ExitWallRun += ExitWallRun;
        uiManager.OnLevelSelection += ChangePOV;

        cameraData = metricsManager.CurrentMetrics.CameraData;
        targetFOV = cameraData.BaseFOV;
        
        // Récupère l’action "FreeLook" de l’Action Map
        freeLookAction = playerInput.actions["FreeLook"];
        if (freeLookAction != null)
        {
            SubscribeFreeLookAction();
        }
    }

    public void SubscribeFreeLookAction()
    {
        freeLookAction.performed += OnFreeLookPerformed;
    }

    public void UnsubscribeFreeLookAction()
    {
        freeLookAction.performed -= OnFreeLookPerformed;
    }

    private void ChangePOV(bool value)
    {
        //TODO : Faire en sorte de changer de point de vue quand on aura un personnage
        //TODO : La caméra ne doit plus pouvoir être bougé
        virtualCamera.gameObject.GetComponent<CinemachineInputProvider>().enabled = !value;
    }

    private void Update()
    {
        if (currentFOV > targetFOV)
        {
            SwitchToBaseFOV();
        }

        if (targetDutch > virtualCamera.m_Lens.Dutch || targetDutch < virtualCamera.m_Lens.Dutch)
        {
            UpdateDutch();
        }
    }

    private void UpdateDutch()
    {
        virtualCamera.m_Lens.Dutch = Mathf.Lerp(virtualCamera.m_Lens.Dutch, targetDutch, metricsManager.CurrentMetrics.WallData.SmoothInclinaison);
    }

    private void OnFreeLookPerformed(InputAction.CallbackContext context)
    {
        var device = context.control.device;

        if (device is Gamepad && lastUsedDevice != "Gamepad")
        {
            ApplySpeed("Gamepad");
            lastUsedDevice = "Gamepad";
        }
        else if ((device is Mouse || device is Pointer) && lastUsedDevice != "Mouse")
        {
            ApplySpeed("Mouse");
            lastUsedDevice = "Mouse";
        }
    }

    private void ApplySpeed(string deviceType)
    {
        float speed = deviceType == "Gamepad" ? controllerSpeed : mouseSpeed / 10;

        if (virtualCamera != null)
        {
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = speed;
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = speed;
        }

        Debug.Log($"[CameraSettingsAdaptation] Appareil : {deviceType}, Vitesse appliquée : {speed}");
    }

    // private void Zoom()
    // {
    //     float zoomValue = inputProvider.GetAxisValue(2) * metricsManager.CurrentPlayerSO.CameraData.ZoomSensitivity;
    //
    //     currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, cameraData.MinimumDistance, cameraData.MaximumDistance);
    //
    //     float currentDistance = framingTransposer.m_CameraDistance;
    //
    //     if (currentDistance == currentTargetDistance)
    //     {
    //         return;
    //     }
    //
    //     float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, cameraData.ZoomSmoothing * Time.deltaTime);
    //
    //     framingTransposer.m_CameraDistance = lerpedZoomValue;
    // }

    private void SetBaseFOV()
    {
        targetFOV = cameraData.BaseFOV;
    }
    
    private void SwitchToBaseFOV()
    {
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView,
            metricsManager.CurrentMetrics.CameraData.BaseFOV, metricsManager.CurrentMetrics.CameraData.SmoothingFactorBaseFOV);
        currentFOV = virtualCamera.m_Lens.FieldOfView;
    }
    
    private void SetTransportFOV(float time)
    {
        virtualCamera.m_Lens.FieldOfView = metricsManager.CurrentMetrics.CameraData.TransitionBaseTransportFOV.Evaluate(time);
        currentFOV = virtualCamera.m_Lens.FieldOfView;
    }

    private void WallRunRotate(float zTilt)
    {
        targetDutch = zTilt;
    }

    private void ExitWallRun()
    {
        targetDutch = 0;
    }

    public void SetControllerSpeed(float newValue)
    {
        controllerSpeed = newValue;
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = controllerSpeed;
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = controllerSpeed;
    }
    
    public void SetMouseSpeed(float newValue)
    {
        mouseSpeed = newValue;
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = mouseSpeed/10;
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = mouseSpeed/10;
    }
    
    public float GetControllerSpeed()
    {
        return controllerSpeed;
    }
    
    public float GetMouseSpeed()
    {
        return mouseSpeed;
    }
}
