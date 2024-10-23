using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.InputSystem;

public class PlayerCameraManager : MonoBehaviour
{
    [field: SerializeField] public CameraLimitPOV CameraLimitFirstPOV { get; private set; }

    [SerializeField] private CinemachineVirtualCamera firstPersonCamera;
    [SerializeField] private CinemachineVirtualCamera thirdPersonCamera;
    [SerializeField] private CinemachineVirtualCamera aimingThirdPersonCamera;

    [SerializeField] private Canvas CrossAirCanvas;

    public bool IsFirstPerson { get; set; } = false;
    public bool IsAiming { get; set; } = false;
    public PlayerInput Input { get; private set; }
    public PlayerMetricsManager MetricsManager { get; private set; }

    private CinemachinePOV cinemachineFirstPOV;
    private CinemachinePOV cinemachineThirdPOV;
    private CinemachineVirtualCamera currentCamera;

    public event Action FirstCameraViewEvent;
    public event Action ThirdCameraViewEvent;
    public event Action AimingCameraViewEvent;

    private void Awake()
    {
        Input = GetComponent<PlayerInput>();
        MetricsManager = GetComponent<PlayerMetricsManager>();
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
        currentCamera = thirdPersonCamera;
    }

    private void OnDisable()
    {
        Input.InputActionInitialize -= SubscriptionInput;
        Input.PlayerActions.CameraToggle.started -= ToggleCameraMode;
    }

    private void ToggleCameraMode(InputAction.CallbackContext context)
    {
        if (MetricsManager.CurrentPlayerSO.AbilityData.ShootData.UseAim)
        {
            IsFirstPerson = !IsFirstPerson;
            Debug.Log("Change Camera Mode");

            if (IsFirstPerson)
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
        else
        {
            IsAiming = !IsAiming;

            if (IsAiming)
            {
                AimingMode();
            }
            else
            {
                ThirdPersonMode();
            }
        }
    }

    private void AimingMode()
    {
        CrossAirCanvas.gameObject.SetActive(true);
        currentCamera.Priority = 0;
        aimingThirdPersonCamera.Priority = 1;
        currentCamera = aimingThirdPersonCamera;
        AimingCameraViewEvent?.Invoke();
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
        CrossAirCanvas.gameObject.SetActive(true);
        currentCamera.Priority = 0;
        firstPersonCamera.Priority = 1;
        currentCamera = firstPersonCamera;
        FirstCameraViewEvent?.Invoke();
    }
    public void ThirdPersonMode()
    {
        CrossAirCanvas.gameObject.SetActive(false);
        currentCamera.Priority = 0;
        thirdPersonCamera.Priority = 1;
        currentCamera = thirdPersonCamera;
        ThirdCameraViewEvent?.Invoke();
    }
}
