using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// S'occupe de mettre � jour les param�tre de la cam�ra 
public class PlayerCameraManager : MonoBehaviour, I_Initializer
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private PlayerMetricsManager metricsManager;
    private CinemachineFramingTransposer framingTransposer;
    private CinemachineInputProvider inputProvider;

    private float currentTargetDistance;
    private PlayerCameraData cameraData;

    public void Init(PlayerReusableStateData reusableStateData)
    {
        metricsManager = GetComponent<PlayerMetricsManager>();
        //framingTransposer = metricsManager.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        inputProvider = metricsManager.GetComponent<CinemachineInputProvider>();
        SetCameraMetrics();
    }

    public void SetCameraMetrics()
    {
        cameraData = metricsManager.CurrentPlayerSO.CameraData;
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = cameraData.ControllerVerticalSpeed;
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = cameraData.ControllerHorizontalSpeed;
    }

    private void Update()
    {
        //Zoom();
    }

    private void Zoom()
    {
        float zoomValue = inputProvider.GetAxisValue(2) * metricsManager.CurrentPlayerSO.CameraData.ZoomSensitivity;

        currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, cameraData.MinimumDistance, cameraData.MaximumDistance);

        float currentDistance = framingTransposer.m_CameraDistance;

        if (currentDistance == currentTargetDistance)
        {
            return;
        }

        float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, cameraData.ZoomSmoothing * Time.deltaTime);

        framingTransposer.m_CameraDistance = lerpedZoomValue;
    }

    public void SetBaseFOV()
    {
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, metricsManager.CurrentPlayerSO.CameraData.BaseFOV, 0.1f);
    }
    
    public void SetTransportFOV()
    {
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, metricsManager.CurrentPlayerSO.CameraData.TransportFOV, 0.1f);
    }
}
