using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.InputSystem;

public class PlayerCameraManager : MonoBehaviour
{
    [field: SerializeField] public CameraLimitPOV CameraLimitFirstPOV { get; private set; }

    [SerializeField] private CinemachineVirtualCamera firstPersonCamera;
    [SerializeField] private CinemachineVirtualCamera thirdPersonCamera;

    private bool isFirstPerson = false;
    private CinemachinePOV cinemachineFirstPOV;
    private CinemachinePOV cinemachineThirdPOV;

    public PlayerInput Input { get; private set; }

    public event Action FirstCameraViewEvent;
    public event Action ThirdCameraViewEvent;

    private void Awake()
    {
        Input = GetComponent<PlayerInput>();
        Input.InputActionInitialize += SubscriptionInput;
        cinemachineFirstPOV = firstPersonCamera.GetCinemachineComponent<CinemachinePOV>();
        cinemachineThirdPOV = thirdPersonCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    private void SubscriptionInput()
    {
        Input.PlayerActions.CameraToggle.started += ToggleCameraMode;
    }

    private void Start()
    {
        firstPersonCamera.Priority = 0;
        thirdPersonCamera.Priority = 1;        
    }

    private void OnDisable()
    {
        Input.InputActionInitialize -= SubscriptionInput;
        Input.PlayerActions.CameraToggle.started -= ToggleCameraMode;
    }

    private void ToggleCameraMode(InputAction.CallbackContext context)
    {
        isFirstPerson = !isFirstPerson;
        Debug.Log("Change Camera Mode");

        if (isFirstPerson)
        {
            SetFirstPersonOrientation();
            FirstPersonMode();
        }
        else
        {
            SetThirdPersonOrientation();
            ThirdPersonMode();
        }
        
    }

    /// <summary>
    /// Set the camera forward to the forward of the gameObject
    /// </summary>
    private void SetFirstPersonOrientation()
    {
        cinemachineFirstPOV.m_HorizontalAxis.Value = transform.eulerAngles.y;
        cinemachineFirstPOV.m_HorizontalAxis.m_MinValue = transform.eulerAngles.y + CameraLimitFirstPOV.MinHorizontalValue;
        cinemachineFirstPOV.m_HorizontalAxis.m_MaxValue = transform.eulerAngles.y + CameraLimitFirstPOV.MaxHorizontalValue;
    }

    private void SetThirdPersonOrientation()
    {
        cinemachineThirdPOV.m_HorizontalAxis.Value = transform.eulerAngles.y;
    }

    private void FirstPersonMode()
    {
        firstPersonCamera.Priority = 1;
        thirdPersonCamera.Priority = 0;
        FirstCameraViewEvent?.Invoke();
    }
    private void ThirdPersonMode()
    {
        firstPersonCamera.Priority = 0;
        thirdPersonCamera.Priority = 1;
        ThirdCameraViewEvent?.Invoke();
    }
}
