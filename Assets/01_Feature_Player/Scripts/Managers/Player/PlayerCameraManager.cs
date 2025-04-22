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

    private PlayerMetricsManager metricsManager;
    private PlayerAbilityManager abilityManager;
    private PlayerInput playerInput;
    private CinemachineFramingTransposer framingTransposer;
    private float currentTargetDistance;
    private PlayerCameraData cameraData;
    
    private InputAction freeLookAction;
    private string lastUsedDevice = "";
    private float currentFOV = 0f;
    private float targetFOV;
    

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void Init(PlayerReusableStateData reusableStateData)
    {
        metricsManager = GetComponent<PlayerMetricsManager>();
        abilityManager = GetComponent<PlayerAbilityManager>();
        abilityManager.AbilityStateMachine.MovePlayer.SpeedModifierEvent += SetTransportFOV;
        abilityManager.AbilityStateMachine.MovePlayer.ExitDash += SetBaseFOV;

        cameraData = metricsManager.CurrentMetrics.CameraData;
        targetFOV = cameraData.BaseFOV;
        
        // Récupère l’action "FreeLook" de l’Action Map
        freeLookAction = playerInput.actions["FreeLook"];
        if (freeLookAction != null)
        {
            freeLookAction.performed += OnFreeLookPerformed;
        }
    }

    private void Update()
    {
        if (currentFOV > targetFOV)
        {
            SwitchToBaseFOV();
        }
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
        float speed = deviceType == "Gamepad" ? cameraData.ControllerSpeed : cameraData.MouseSpeed / 10;

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
        Camera.main.transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
}
