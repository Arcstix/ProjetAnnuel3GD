using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.InputSystem;

public class PlayerCameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCameraBase firstPersonCamera;
    [SerializeField] private CinemachineVirtualCameraBase thirdPersonCamera;

    private bool isFirstPerson = false;

    public PlayerInput Input { get; private set; }


    private void Awake()
    {
        Input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        firstPersonCamera.Priority = 0;
        thirdPersonCamera.Priority = 1;
        Input.PlayerActions.CameraToggle.started += ToggleCameraMode;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        Input.PlayerActions.CameraToggle.started -= ToggleCameraMode;
    }

    private void ToggleCameraMode(InputAction.CallbackContext context)
    {
        isFirstPerson = !isFirstPerson;
        Debug.Log("Change Camera Mode");

        if (isFirstPerson)
        {
            FirstPersonMode();
        }
        else
        {
            ThirdPersonMode();
        }
    }

    private void FirstPersonMode()
    {
        firstPersonCamera.Priority = 1;
        thirdPersonCamera.Priority = 0;
    }
    private void ThirdPersonMode()
    {
        firstPersonCamera.Priority = 0;
        thirdPersonCamera.Priority = 1;
    }
}
