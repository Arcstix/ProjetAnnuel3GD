using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputAction InputActions { get; private set; }
    public PlayerInputAction.PlayerActions PlayerActions { get; private set; }

    public event Action InputActionInitialize;

    private void Awake()
    {
        InputActions = new PlayerInputAction();

        PlayerActions = InputActions.Player;
    }

    private void OnEnable()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Weapon/ShootV1");
        InputActions.Enable();
        InputActionInitialize?.Invoke();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}
