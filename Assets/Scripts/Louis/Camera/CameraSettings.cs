using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] private float defaultDistance = 6f;
    [SerializeField] [Range(0f, 10f)] private float minimumDistance = 1f;
    [SerializeField] [Range(0f, 10f)] private float maximumDistance = 6f;

    [SerializeField] [Range(0f, 10f)] private float smoothing = 4f;
    [SerializeField] [Range(0f, 10f)] private float zoomSensitivity = 1f;

    [SerializeField] [Range(0f, 10f)] private float controllerVerticalSpeed = 4f;
    [SerializeField] [Range(0f, 10f)] private float controllerHorizontalSpeed = 1f;
    [SerializeField] [Range(0f, 0.5f)] private float mouseVerticalSpeed = 0.05f;
    [SerializeField] [Range(0f, 0.5f)] private float mouseHorizontalSpeed = 0.2f;

    [SerializeField] private bool useController = false;
    [SerializeField] private bool isFirstPerson = false;

    private CinemachineFramingTransposer framingTransposer;
    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera virtualCamera;

    private float currentTargetDistance;

    private void Awake()
    {
        framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        inputProvider = GetComponent<CinemachineInputProvider>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        currentTargetDistance = defaultDistance;
    }

    private void Start()
    {
        ChangeSettingsCamera();
    }

    public void ChangeSettingsCamera()
    {
        if (useController)
        {
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = controllerVerticalSpeed;
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = controllerHorizontalSpeed;
        }
        else
        {
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = mouseVerticalSpeed;
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = mouseHorizontalSpeed;
        }
    }

    private void Update()
    {
        if (!isFirstPerson)
        {
            Zoom();
        }        
    }

    private void Zoom()
    {
        float zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;

        currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minimumDistance, maximumDistance);

        float currentDistance = framingTransposer.m_CameraDistance;

        if(currentDistance == currentTargetDistance)
        {
            return;
        }

        float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);

        framingTransposer.m_CameraDistance = lerpedZoomValue;
    }
}
