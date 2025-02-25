using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMetricsManager))]
public class PlayerManager : MonoBehaviour
{
    [field: Header("References")]
    public PlayerMetricsManager Metrics { get; private set; }

    public Rigidbody Rb { get; private set; }

    public Camera Cam { get; private set; }

    public PlayerInput Input { get; private set; }

    public PlayerCameraManager CameraManager { get; private set; }

    protected virtual void Awake()
    {
        Metrics = GetComponent<PlayerMetricsManager>();

        Input = GetComponent<PlayerInput>();

        Cam = Camera.main;

        Rb = GetComponent<Rigidbody>();

        CameraManager = GetComponent<PlayerCameraManager>();
    }
}
